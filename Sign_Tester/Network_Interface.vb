Option Explicit On

Imports System
Imports System.Net
Imports System.Net.Sockets
Imports System.IO
Imports System.Text
Imports System.Threading

'----------------------------------------------------------------------------------------------

Public Class State_Object
    ' Client socket
    Public Work_Socket As Socket
End Class


Public Class ASynchronousSocketListener

    Public Shared allDone = New ManualResetEvent(False)
    Public Shared run_TCP As Boolean = False
    Public Shared localPort As Integer = 0

    Dim mySocket As Socket

    Public Function Start_TCP(port As Integer) As Boolean

        If port = 0 Then
            run_TCP = False
        Else
            localPort = port
            Sign_Tester.Log_It("Local address at port " & port)
            Sign_Tester.Log_It("Connecting...")

            run_TCP = True
            Dim myThread As Thread
            myThread = New Thread(AddressOf StartListening)
            myThread.IsBackground = True
            myThread.Start()

            allDone.Reset()

        End If
        Start_TCP = run_TCP

    End Function

    Public Sub Stop_TCP()

        run_TCP = False
        mySocket.Close()
        Sign_Tester.Log_It("Disconnect the listener...")

    End Sub

    Public Sub StartListening()

        mySocket = New Socket(SocketType.Stream, ProtocolType.Tcp)
        Dim localEP As IPEndPoint = New IPEndPoint(0, localPort)

        Try
            mySocket.Bind(localEP)
            mySocket.Listen(10)

            While run_TCP
                allDone.Reset()
                mySocket.BeginAccept(New AsyncCallback(AddressOf acceptCallback), mySocket)
                allDone.WaitOne()
            End While

        Catch e As Exception
            Sign_Tester.myLog(0) = e.ToString()
        End Try

        run_TCP = False

    End Sub     '   StartListening

    Public Shared Sub acceptCallback(ar As IAsyncResult)

        ' Get the socket that handles the client request.
        Dim listener As Socket = CType(ar.AsyncState, Socket)
        Dim CBhandler As Socket

        If run_TCP Then

            CBhandler = listener.EndAccept(ar)

            ' Signal the main thread to continue.
            allDone.Set()

            ' Create the state object.
            Dim state As New State_Object()
            state.Work_Socket = CBhandler
            Sign_Tester.myState(0).Work_Socket = CBhandler

            CBhandler.BeginReceive(Sign_Tester.Rx_Comms(0).Buffer, 0, (Packet_Data.Packet_Size * 2) + 20, 0, AddressOf readCallback, state)

        End If

    End Sub     '   acceptCallback

    '--------------------------------------------------------------------------------------------

    Public Shared Sub readCallback(ar As IAsyncResult)

        Dim state As State_Object = CType(ar.AsyncState, State_Object)
        Dim handler As Socket = state.Work_Socket

        ' Read data from the client socket. 
        Dim read As Integer = handler.EndReceive(ar)

        ' Data was read from the client socket.
        If read > 0 Then
            Sign_Tester.Rx_Comms(0).Num_Bytes = read
        End If
        handler.BeginReceive(Sign_Tester.Rx_Comms(0).Buffer, 0, (Packet_Data.Packet_Size * 2) + 20, 0, AddressOf readCallback, state)

    End Sub     '   readCallback

    '--------------------------------------------------------------------------------------------

    Public Shared Sub SendPacket(ByRef Tx As Comms_Buffer)

        Send(Sign_Tester.myState(0).Work_Socket, Tx.Buffer, Tx.Num_Bytes)

    End Sub     '   SendPacket

    '--------------------------------------------------------------------------------------------

    Private Shared Sub Send(handler As Socket, ByRef byteData() As Byte, size As Integer)

        '   Begin sending the data to the remote device.  
        handler.BeginSend(byteData, 0, size, 0, New AsyncCallback(AddressOf SendCallback), handler)

    End Sub

    '--------------------------------------------------------------------------------------------

    Private Shared Sub SendCallback(ar As IAsyncResult)

        Try
            '   Retrieve the socket from the state object.  
            Dim handler As Socket = CType(ar.AsyncState, Socket)
            Dim bytesSent As Integer
            '   Complete sending the data to the remote device.  
            bytesSent = handler.EndSend(ar)

        Catch e As Exception
            Sign_Tester.myLog(0) = e.ToString()
        End Try

    End Sub

End Class
