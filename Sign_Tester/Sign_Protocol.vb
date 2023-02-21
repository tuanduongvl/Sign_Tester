Option Explicit On

'============================================================================

Public Class Packet_Data

    '   constants
    Public Const Packet_Size As UInteger = 1024

    '   class members
    Private Mi_Code As UInteger
    Private Num_Bytes As UInteger = 0
    Private Packet(Packet_Size) As Byte

    '   clear buffer
    Public Sub Clear_Packet()
        Num_Bytes = 0
    End Sub

    '   get size of data in packet
    Public Function Get_Size() As UInteger
        Get_Size = Num_Bytes
    End Function

    '   set Mi-Code
    Public Sub Set_Mi_Code(MiCode As UInteger)
        Mi_Code = MiCode
    End Sub

    '   get Mi-Code
    Public Function Get_Mi_Code()
        Get_Mi_Code = Mi_Code
    End Function

    '   add a byte to the packet
    Public Sub Add_Byte(Data As Byte)
        If (Num_Bytes < Packet_Size) Then
            Packet(Num_Bytes) = Data
            Num_Bytes = Num_Bytes + 1
        End If
    End Sub

    'read a byte from the packet
    Public Function Read_Byte(Index As UInteger) As Byte
        If (Index < Num_Bytes) Then
            Read_Byte = Packet(Index)
        Else
            Read_Byte = 0
        End If
    End Function

End Class

'============================================================================

Public Class CRC_CCITT16

    Private CRC As UInt16 = 0

    '   CCITT 16 look up data
    Private ReadOnly ccitt_16_table() As UInt16 =
{
        &H0, &H1021, &H2042, &H3063, &H4084, &H50A5, &H60C6, &H70E7,
        &H8108, &H9129, &HA14A, &HB16B, &HC18C, &HD1AD, &HE1CE, &HF1EF,
        &H1231, &H210, &H3273, &H2252, &H52B5, &H4294, &H72F7, &H62D6,
        &H9339, &H8318, &HB37B, &HA35A, &HD3BD, &HC39C, &HF3FF, &HE3DE,
        &H2462, &H3443, &H420, &H1401, &H64E6, &H74C7, &H44A4, &H5485,
        &HA56A, &HB54B, &H8528, &H9509, &HE5EE, &HF5CF, &HC5AC, &HD58D,
        &H3653, &H2672, &H1611, &H630, &H76D7, &H66F6, &H5695, &H46B4,
        &HB75B, &HA77A, &H9719, &H8738, &HF7DF, &HE7FE, &HD79D, &HC7BC,
        &H48C4, &H58E5, &H6886, &H78A7, &H840, &H1861, &H2802, &H3823,
        &HC9CC, &HD9ED, &HE98E, &HF9AF, &H8948, &H9969, &HA90A, &HB92B,
        &H5AF5, &H4AD4, &H7AB7, &H6A96, &H1A71, &HA50, &H3A33, &H2A12,
        &HDBFD, &HCBDC, &HFBBF, &HEB9E, &H9B79, &H8B58, &HBB3B, &HAB1A,
        &H6CA6, &H7C87, &H4CE4, &H5CC5, &H2C22, &H3C03, &HC60, &H1C41,
        &HEDAE, &HFD8F, &HCDEC, &HDDCD, &HAD2A, &HBD0B, &H8D68, &H9D49,
        &H7E97, &H6EB6, &H5ED5, &H4EF4, &H3E13, &H2E32, &H1E51, &HE70,
        &HFF9F, &HEFBE, &HDFDD, &HCFFC, &HBF1B, &HAF3A, &H9F59, &H8F78,
        &H9188, &H81A9, &HB1CA, &HA1EB, &HD10C, &HC12D, &HF14E, &HE16F,
        &H1080, &HA1, &H30C2, &H20E3, &H5004, &H4025, &H7046, &H6067,
        &H83B9, &H9398, &HA3FB, &HB3DA, &HC33D, &HD31C, &HE37F, &HF35E,
        &H2B1, &H1290, &H22F3, &H32D2, &H4235, &H5214, &H6277, &H7256,
        &HB5EA, &HA5CB, &H95A8, &H8589, &HF56E, &HE54F, &HD52C, &HC50D,
        &H34E2, &H24C3, &H14A0, &H481, &H7466, &H6447, &H5424, &H4405,
        &HA7DB, &HB7FA, &H8799, &H97B8, &HE75F, &HF77E, &HC71D, &HD73C,
        &H26D3, &H36F2, &H691, &H16B0, &H6657, &H7676, &H4615, &H5634,
        &HD94C, &HC96D, &HF90E, &HE92F, &H99C8, &H89E9, &HB98A, &HA9AB,
        &H5844, &H4865, &H7806, &H6827, &H18C0, &H8E1, &H3882, &H28A3,
        &HCB7D, &HDB5C, &HEB3F, &HFB1E, &H8BF9, &H9BD8, &HABBB, &HBB9A,
        &H4A75, &H5A54, &H6A37, &H7A16, &HAF1, &H1AD0, &H2AB3, &H3A92,
        &HFD2E, &HED0F, &HDD6C, &HCD4D, &HBDAA, &HAD8B, &H9DE8, &H8DC9,
        &H7C26, &H6C07, &H5C64, &H4C45, &H3CA2, &H2C83, &H1CE0, &HCC1,
        &HEF1F, &HFF3E, &HCF5D, &HDF7C, &HAF9B, &HBFBA, &H8FD9, &H9FF8,
        &H6E17, &H7E36, &H4E55, &H5E74, &H2E93, &H3EB2, &HED1, &H1EF0
}

    '   clear CRC
    Public Sub Clear_CRC()
        CRC = 0
    End Sub

    '   get CRC as a UInt16
    Public Function Get_CRC() As UInt16
        Get_CRC = CRC
    End Function

    '   get high byte of CRC as a Byte
    Public Function Get_CRC_Hi() As Byte
        Get_CRC_Hi = CType((CRC >> 8) And &HFF, Byte)
    End Function

    '   get low byte of CRC as a Byte
    Public Function Get_CRC_Lo() As Byte
        Get_CRC_Lo = CType(CRC And &HFF, Byte)
    End Function

    '   update CRC
    Public Sub Do_CRC(Data As Byte)
        CRC = ((CRC << 8) And &HFF00) Xor ccitt_16_table(((CRC >> 8) Xor CType(Data, UInt16)) And &HFF)
    End Sub

End Class

'============================================================================

Public Class Comms_Buffer

    '   constants
    Private ReadOnly Hex_Ch() As Byte = {&H30, &H31, &H32, &H33, &H34, &H35, &H36, &H37, &H38, &H39, &H41, &H42, &H43, &H44, &H45, &H46}

    '   class members
    Private CRC As New CRC_CCITT16
    Public Num_Bytes As UInteger = 0
    Public Buffer((Packet_Data.Packet_Size * 2) + 20) As Byte

    '   clear buffer
    Public Sub Clear_Buffer()
        Clear_CRC()
        Num_Bytes = 0
    End Sub

    '   clear CRC
    Public Sub Clear_CRC()
        CRC.Clear_CRC()
    End Sub

    '   get size of data in packet
    Public Function Get_Size() As UInteger
        Get_Size = Num_Bytes
    End Function

    '   get refereence to buffer
    Public Function Get_Buffer() As Byte()
        Get_Buffer = Buffer
    End Function

    '   add a byte to the packet
    Public Sub Add_Byte(Data As Byte)
        If (Num_Bytes < ((Packet_Data.Packet_Size * 2) + 20)) Then
            Buffer(Num_Bytes) = Data
            Num_Bytes = Num_Bytes + 1
            CRC.Do_CRC(Data)
        End If
    End Sub

    '   add HEX bytes to the packet
    Public Sub Add_Hex_Bytes(Data As Byte)
        Add_Byte(Hex_Ch((Data >> 4) And &HF))
        Add_Byte(Hex_Ch(Data And &HF))
    End Sub

    '   get CRC as a UInt16
    Public Function Get_CRC() As UInt16
        Get_CRC = CRC.Get_CRC()
    End Function

    'read a byte from the packet
    Public Function Read_Byte(Index As UInteger) As Byte
        If (Index < Num_Bytes) Then
            Read_Byte = Buffer(Index)
        Else
            Read_Byte = 0
        End If
    End Function

End Class

'============================================================================

Public Class Rx_Tx_Data

    '   constants
    Private Const SOH As Byte = &H1
    Private Const STX As Byte = &H2
    Private Const ETX As Byte = &H3
    Private Const ACK As Byte = &H6
    Private Const NAK As Byte = &H15

    '   class members
    Private NR As Integer = 0
    Private NS As Integer = 0
    Private Adrs As Integer = 0
    Private Rx_Data As New Packet_Data
    Private Tx_Data As New Packet_Data

    '   flush a received TCP packet
    Public Sub Rx_Flush(ByRef Rx As Comms_Buffer)
        Rx.Num_Bytes = 0
    End Sub

    '   receive a TCP packet
    '
    '   NOTE :  This does not handle a NAK packet
    '
    Public Sub Rx_Packet(ByRef Rx As Comms_Buffer)

        Dim ii As UInteger
        Dim jj As UInteger

        Dim soh_pos As UInteger
        Dim stx_pos As UInteger
        Dim etx_pos As UInteger

        ii = 0
        While (ii < Rx.Num_Bytes) And (Rx.Buffer(ii) <> SOH)
            ii = ii + 1
        End While
        If ii < Rx.Num_Bytes Then
            soh_pos = ii
            While (ii < Rx.Num_Bytes) And (Rx.Buffer(ii) <> STX)
                ii = ii + 1
            End While
            If ii < Rx.Num_Bytes Then
                stx_pos = ii
                While (ii < Rx.Num_Bytes) And (Rx.Buffer(ii) <> ETX)
                    ii = ii + 1
                End While
                If ii < Rx.Num_Bytes Then
                    etx_pos = ii

                    '   have a whole Rx packet
                    NS = Get_Hex_Bytes(Rx.Buffer, soh_pos + 1)
                    NR = Get_Hex_Bytes(Rx.Buffer, soh_pos + 3)
                    Adrs = Get_Hex_Bytes(Rx.Buffer, soh_pos + 5)
                    Sign_Tester.lblAdrs.Text = Adrs.ToString()
                    Rx_Data.Clear_Packet()
                    Rx_Data.Set_Mi_Code(Get_Hex_Bytes(Rx.Buffer, stx_pos + 1))
                    ii = stx_pos + 3
                    jj = etx_pos - 4
                    If ii < jj Then
                        jj = jj - 2
                        For ii = ii To jj Step 2
                            Rx_Data.Add_Byte(Get_Hex_Bytes(Rx.Buffer, ii))
                        Next
                    End If
                    Process_MI_Codes.Analyse_Rx_Packet(Rx_Data, Tx_Data)

                    '   send the Tx Packet
                    Dim rx_MI_Code As Integer
                    rx_MI_Code = Rx_Data.Get_Mi_Code()
                    Dim tx_MI_Code As Integer
                    tx_MI_Code = Tx_Data.Get_Mi_Code()
                    Sign_Tester.Log_It("Rx " & rx_MI_Code.ToString("X2") & "       Tx " & tx_MI_Code.ToString("X2"))

                End If
            End If
        End If
        Rx.Num_Bytes = 0

    End Sub

    '   convert two ascii-hex bytes into one byte
    Public Function Get_Hex_Bytes(ByRef Rx() As Byte, pos As UInteger) As Byte

        Get_Hex_Bytes = (Get_Hex_Byte(Rx, pos) * 16) + Get_Hex_Byte(Rx, pos + 1)

    End Function

    '   convert one ascii-hex byte into one byte
    Private Function Get_Hex_Byte(ByRef Rx() As Byte, pos As UInteger) As Byte

        Dim ch As Byte

        ch = Rx(pos)
        If ((ch >= &H30) And (ch <= &H39)) Then
            Get_Hex_Byte = ch - &H30
        Else
            If ((ch >= &H41) And (ch <= &H46)) Then
                Get_Hex_Byte = ch - &H41 + 10
            Else
                Get_Hex_Byte = 0
            End If
        End If

    End Function

    '   send a TCP packet
    Public Sub Tx_Packet(ByRef Tx As Comms_Buffer)

        '   variables
        Dim tx_crc As UInt16
        Dim ii As UInteger

        '   first the ACK packet
        Tx.Clear_Buffer()
        Tx.Add_Byte(ACK)
        NR = NR + 1
        If NR >= 256 Then
            NR = 1
        End If
        Tx.Add_Hex_Bytes(NR)
        Tx.Add_Hex_Bytes(Adrs)
        tx_crc = Tx.Get_CRC
        Tx.Add_Hex_Bytes(CType((tx_crc >> 8) And &HFF, Byte))
        Tx.Add_Hex_Bytes(CType(tx_crc And &HFF, Byte))
        Tx.Add_Byte(ETX)

        '   add on the SOH packet
        Tx.Clear_CRC()
        Tx.Add_Byte(SOH)
        Tx.Add_Hex_Bytes(NS)
        Tx.Add_Hex_Bytes(NR)
        Tx.Add_Hex_Bytes(Adrs)
        Tx.Add_Byte(STX)
        Tx.Add_Hex_Bytes(Tx_Data.Get_Mi_Code())
        If Tx_Data.Get_Size() > 0 Then
            For ii = 0 To (Tx_Data.Get_Size() - 1)
                Tx.Add_Hex_Bytes(Tx_Data.Read_Byte(ii))
            Next
        End If
        tx_crc = Tx.Get_CRC
        Tx.Add_Hex_Bytes(CType((tx_crc >> 8) And &HFF, Byte))
        Tx.Add_Hex_Bytes(CType(tx_crc And &HFF, Byte))
        Tx.Add_Byte(ETX)

    End Sub

End Class
