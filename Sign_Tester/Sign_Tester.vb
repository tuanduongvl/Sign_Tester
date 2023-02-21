Option Explicit On

Imports System.String

Public Class Sign_Tester

    Enum Mode As Integer
        DoInit
        SetTCP
        SetSignType
        DoConfig
        DoRun
    End Enum

    Public Const Max_Signs As Integer = 12
    Public Const Max_Groups As Integer = 6

    Public myASyncSocket As ASynchronousSocketListener

    Public Shared myLog() As String = {New String("")}
    Public Shared myState() As State_Object = {New State_Object}
    Public Shared Rx_Comms() As Comms_Buffer = {New Comms_Buffer}
    Public Shared Tx_Comms() As Comms_Buffer = {New Comms_Buffer}
    Public Rx_Tx As Rx_Tx_Data = New Rx_Tx_Data
    Public runMode As Mode = Mode.DoInit

    Public Class Controller_Data

        Public Frame_Loaded As Boolean
        Public Frame_Text As String
        Public Frame_Packet As Packet_Data

        Public Message_Loaded As Boolean
        Public Message_Text As String
        'Public Message_Step As Integer
        Public Message_Frame_ID(6) As Integer
        Public Message_Packet As Packet_Data

    End Class

    Public Class Group_Data

        '   group control values
        Public Fac_Sw As Integer
        Public ESI_Sw As Integer

        '   switches status
        Public grpGroup As GroupBox
        Public cmbFac_Sw As ComboBox
        Public cmbESI_Sw As ComboBox

    End Class

    Public Class Sign_Data

        '   protocol values
        Public Sign_ID As UInteger
        Public Group_ID As UInteger
        Public Enabled As Boolean = True        '   also controller on-line
        Public Error_Code As UInteger           '   also controller error code
        Public Prev_Error As UInteger
        Public Error_Time As System.DateTime
        Public Frame_ID As UInteger
        Public Set_Frame_ID As UInteger
        Public Message_ID As UInteger
        Public Set_Message_ID As UInteger
        Public Message_Step As UInteger

        '   configuration controls
        Public lblSign As Label
        Public nudGroupID As NumericUpDown

        '   status controls
        Public chkEnable As CheckBox            '   also controller on-line
        Public cmbError As ComboBox             '   also controller error code
        Public lblFrame As Label
        Public lblMessage As Label

    End Class

    Public Shared Controller_Status(255) As Controller_Data

    Public Sign_Status() As Sign_Data =
    {
        New Sign_Data,                          '   index 0 is controller data
        New Sign_Data,                          '   index 1 is sign 1 data
        New Sign_Data,
        New Sign_Data,
        New Sign_Data,
        New Sign_Data,
        New Sign_Data,
        New Sign_Data,
        New Sign_Data,
        New Sign_Data,
        New Sign_Data,
        New Sign_Data,
        New Sign_Data                           '   index 12 is sign 12 data
    }

    Public Group_Status() As Group_Data =
    {
        New Group_Data,                         '   index 0 is unused
        New Group_Data,                         '   index 1 is group 1 data
        New Group_Data,
        New Group_Data,
        New Group_Data,
        New Group_Data,
        New Group_Data                          '   index 6 is group 6 data
    }

    Private Sub Sign_Tester_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        tmrLoop.Enabled = False

        runMode = Mode.SetTCP
        numTCPPort.Value = 38400
        btnStartStop.Text = "Set TCP Port"
        lblSignType.Enabled = False
        cmbSignType.Enabled = False
        chkCtrl_OnLine.Checked = False

        txtProtocol.ScrollToCaret()

        nudNumSigns.Value = 1
        Init_Controller_Status()
        Init_Sign_Status()
        Init_Group_Status()
        Load_Error_Codes()
        Set_Ctrl_Enabled_TCP()
        Enable_Fields(False)
        Change_Num_Signs()
        Change_Num_Groups()
        '   cmbSignType.SelectedIndex = 1

    End Sub

    Private Sub Enable_Fields(enabled As Boolean)
        lblNumSigns.Enabled = enabled
        chkCtrl_OnLine.Enabled = enabled
        lblAddress.Enabled = enabled
        lblAdrs.Enabled = enabled
        cmbCtrlErr.Enabled = enabled
        lblCtrlErr.Enabled = enabled
        lblFacSw.Enabled = enabled
        lblSign1.Enabled = enabled
        nudGroupID1.Enabled = enabled
        chkSignEn1.Enabled = enabled
        cmbSignErr1.Enabled = enabled
        lblSignFrame1.Enabled = enabled
        lblSignMsg1.Enabled = enabled
        txtProtocol.Enabled = enabled
    End Sub
    Private Sub Init_Controller_Status()
        Dim ii As Integer
        For ii = 0 To 255
            Controller_Status(ii) = New Controller_Data
        Next
        Clear_Frame_Msg()
    End Sub

    Public Sub Clear_Frame_Msg()
        Dim ii As Integer
        Dim jj As Integer
        For ii = 0 To 255
            Controller_Status(ii).Frame_Loaded = False
            Controller_Status(ii).Frame_Text = ""
            Controller_Status(ii).Frame_Packet = New Packet_Data
            Controller_Status(ii).Message_Loaded = False
            For jj = 0 To 6
                Controller_Status(ii).Message_Frame_ID(jj) = 0
            Next
            Controller_Status(ii).Message_Packet = New Packet_Data
        Next
        Controller_Status(0).Frame_Loaded = True
        Controller_Status(0).Frame_Text = "0"
        Controller_Status(0).Message_Loaded = True
        Controller_Status(0).Message_Text = "0"
    End Sub

    Private Sub Init_Sign_Status()
        Init_Sign_ID()
        Init_Group_ID_NUD()
        Init_Sign_Label()
        Init_Sign_Enable()
        Init_Sign_Error()
        Init_Sign_Frame()
        Init_Sign_Message()
    End Sub

    Public Sub Init_Sign_ID()
        Dim ii As Integer
        For ii = 0 To Max_Signs
            Sign_Status(ii).Sign_ID = ii
            Sign_Status(ii).Group_ID = 1
            Sign_Status(ii).Enabled = True
            Sign_Status(ii).Error_Code = 0
            Sign_Status(ii).Prev_Error = 0
            Sign_Status(ii).Frame_ID = 0
            Sign_Status(ii).Set_Frame_ID = 0
            Sign_Status(ii).Message_ID = 0
            Sign_Status(ii).Set_Message_ID = 0
            Sign_Status(ii).Message_Step = 0
        Next
    End Sub

    Public Sub Init_Group_ID_NUD()
        Sign_Status(1).nudGroupID = nudGroupID1
        Sign_Status(2).nudGroupID = nudGroupID2
        Sign_Status(3).nudGroupID = nudGroupID3
        Sign_Status(4).nudGroupID = nudGroupID4
        Sign_Status(5).nudGroupID = nudGroupID5
        Sign_Status(6).nudGroupID = nudGroupID6
        Sign_Status(7).nudGroupID = nudGroupID7
        Sign_Status(8).nudGroupID = nudGroupID8
        Sign_Status(9).nudGroupID = nudGroupID9
        Sign_Status(10).nudGroupID = nudGroupID10
        Sign_Status(11).nudGroupID = nudGroupID11
        Sign_Status(12).nudGroupID = nudGroupID12
    End Sub

    Public Sub Init_Sign_Label()
        Sign_Status(1).lblSign = lblSign1
        Sign_Status(2).lblSign = lblSign2
        Sign_Status(3).lblSign = lblSign3
        Sign_Status(4).lblSign = lblSign4
        Sign_Status(5).lblSign = lblSign5
        Sign_Status(6).lblSign = lblSign6
        Sign_Status(7).lblSign = lblSign7
        Sign_Status(8).lblSign = lblSign8
        Sign_Status(9).lblSign = lblSign9
        Sign_Status(10).lblSign = lblSign10
        Sign_Status(11).lblSign = lblSign11
        Sign_Status(12).lblSign = lblSign12
    End Sub

    Public Sub Init_Sign_Enable()
        '   controller on-line
        Sign_Status(0).chkEnable = chkCtrl_OnLine

        '   sign enabled
        Sign_Status(1).chkEnable = chkSignEn1
        Sign_Status(2).chkEnable = chkSignEn2
        Sign_Status(3).chkEnable = chkSignEn3
        Sign_Status(4).chkEnable = chkSignEn4
        Sign_Status(5).chkEnable = chkSignEn5
        Sign_Status(6).chkEnable = chkSignEn6
        Sign_Status(7).chkEnable = chkSignEn7
        Sign_Status(8).chkEnable = chkSignEn8
        Sign_Status(9).chkEnable = chkSignEn9
        Sign_Status(10).chkEnable = chkSignEn10
        Sign_Status(11).chkEnable = chkSignEn11
        Sign_Status(12).chkEnable = chkSignEn12
    End Sub

    Public Sub Init_Sign_Error()
        '   controller error code
        Sign_Status(0).cmbError = cmbCtrlErr

        '   sign error code
        Sign_Status(1).cmbError = cmbSignErr1
        Sign_Status(2).cmbError = cmbSignErr2
        Sign_Status(3).cmbError = cmbSignErr3
        Sign_Status(4).cmbError = cmbSignErr4
        Sign_Status(5).cmbError = cmbSignErr5
        Sign_Status(6).cmbError = cmbSignErr6
        Sign_Status(7).cmbError = cmbSignErr7
        Sign_Status(8).cmbError = cmbSignErr8
        Sign_Status(9).cmbError = cmbSignErr9
        Sign_Status(10).cmbError = cmbSignErr10
        Sign_Status(11).cmbError = cmbSignErr11
        Sign_Status(12).cmbError = cmbSignErr12
    End Sub

    Public Sub Init_Sign_Frame()
        Sign_Status(1).lblFrame = lblSignFrame1
        Sign_Status(2).lblFrame = lblSignFrame2
        Sign_Status(3).lblFrame = lblSignFrame3
        Sign_Status(4).lblFrame = lblSignFrame4
        Sign_Status(5).lblFrame = lblSignFrame5
        Sign_Status(6).lblFrame = lblSignFrame6
        Sign_Status(7).lblFrame = lblSignFrame7
        Sign_Status(8).lblFrame = lblSignFrame8
        Sign_Status(9).lblFrame = lblSignFrame9
        Sign_Status(10).lblFrame = lblSignFrame10
        Sign_Status(11).lblFrame = lblSignFrame11
        Sign_Status(12).lblFrame = lblSignFrame12
    End Sub

    Public Sub Init_Sign_Message()
        Sign_Status(1).lblMessage = lblSignMsg1
        Sign_Status(2).lblMessage = lblSignMsg2
        Sign_Status(3).lblMessage = lblSignMsg3
        Sign_Status(4).lblMessage = lblSignMsg4
        Sign_Status(5).lblMessage = lblSignMsg5
        Sign_Status(6).lblMessage = lblSignMsg6
        Sign_Status(7).lblMessage = lblSignMsg7
        Sign_Status(8).lblMessage = lblSignMsg8
        Sign_Status(9).lblMessage = lblSignMsg9
        Sign_Status(10).lblMessage = lblSignMsg10
        Sign_Status(11).lblMessage = lblSignMsg11
        Sign_Status(12).lblMessage = lblSignMsg12
    End Sub

    Public Sub Init_Group_Status()
        Init_Group_Values()
        Init_Group_Box()
        Init_Group_Fac()
        Init_Group_ESI()
    End Sub

    Public Sub Init_Group_Values()
        Dim ii As Integer
        For ii = 1 To Max_Groups
            Group_Status(ii).Fac_Sw = -1
            Group_Status(ii).ESI_Sw = -1
        Next
    End Sub

    Public Sub Init_Group_Box()
        Group_Status(1).grpGroup = grpGroup1
        Group_Status(2).grpGroup = grpGroup2
        Group_Status(3).grpGroup = grpGroup3
        Group_Status(4).grpGroup = grpGroup4
        Group_Status(5).grpGroup = grpGroup5
        Group_Status(6).grpGroup = grpGroup6
    End Sub

    Public Sub Init_Group_Fac()
        Group_Status(1).cmbFac_Sw = cmbFacSw1
        Group_Status(2).cmbFac_Sw = cmbFacSw2
        Group_Status(3).cmbFac_Sw = cmbFacSw3
        Group_Status(4).cmbFac_Sw = cmbFacSw4
        Group_Status(5).cmbFac_Sw = cmbFacSw5
        Group_Status(6).cmbFac_Sw = cmbFacSw6
    End Sub

    Public Sub Init_Group_ESI()
        Group_Status(1).cmbESI_Sw = cmbESI1
        Group_Status(2).cmbESI_Sw = cmbESI2
        Group_Status(3).cmbESI_Sw = cmbESI3
        Group_Status(4).cmbESI_Sw = cmbESI4
        Group_Status(5).cmbESI_Sw = cmbESI5
        Group_Status(6).cmbESI_Sw = cmbESI6
    End Sub

    Private Sub Load_Error_Codes()

        Dim ii As Integer

        txtProtocol.Text = ""

        '   Sign Type
        cmbSignType.Items.Add("ISLUS")
        cmbSignType.Items.Add("TMS")
        cmbSignType.Items.Add("VMS")
        cmbSignType.Items.Add("CMS")

        '   Controller Error Codes
        cmbCtrlErr.Items.Add("00 No Error")
        cmbCtrlErr.Items.Add("01 Primary Power Failure")
        cmbCtrlErr.Items.Add("02 Communications Timeout")
        cmbCtrlErr.Items.Add("03 Memory Error")
        cmbCtrlErr.Items.Add("04 Battery Failure")
        cmbCtrlErr.Items.Add("09 Over Temp (Fan Failure)")
        cmbCtrlErr.Items.Add("0A Under Temp (Heater Failure)")
        cmbCtrlErr.Items.Add("0D Controller Reset")
        cmbCtrlErr.Items.Add("0E Battery Low")
        cmbCtrlErr.Items.Add("10 Facility Switch Override")
        cmbCtrlErr.Items.Add("14 Equipment Over-Temp")
        cmbCtrlErr.Items.Add("1C Display Timeout")
        cmbCtrlErr.Items.Add("24 Door Open")
        cmbCtrlErr.Items.Add("FF Fault Cleared")

        '   Sign Error Codes
        For ii = 1 To Max_Signs
            Sign_Status(ii).cmbError.Enabled = False
            Sign_Status(ii).cmbError.Items.Add("00 No Error")
            Sign_Status(ii).cmbError.Items.Add("03 Memory Error")
            Sign_Status(ii).cmbError.Items.Add("05 Internal Comms Failure")
            Sign_Status(ii).cmbError.Items.Add("07 Single LED Failure")
            Sign_Status(ii).cmbError.Items.Add("08 MULTI LED Failure")
            Sign_Status(ii).cmbError.Items.Add("09 Over-Temp (Fan Failure)")
            Sign_Status(ii).cmbError.Items.Add("0A Under-Temp (Heater Failure)")
            Sign_Status(ii).cmbError.Items.Add("0B Conspicuity Failure")
            Sign_Status(ii).cmbError.Items.Add("0C Luminance Controller Failure")
            Sign_Status(ii).cmbError.Items.Add("0F Powered Off By Command")
            Sign_Status(ii).cmbError.Items.Add("10 Facility Switch Override")
            Sign_Status(ii).cmbError.Items.Add("11 Display Driver Failure")
            Sign_Status(ii).cmbError.Items.Add("12 Sign Firmware Mismatch")
            Sign_Status(ii).cmbError.Items.Add("14 Equipment Over Temperature")
            Sign_Status(ii).cmbError.Items.Add("FF Fault Cleared")
        Next

        '   group options
        For ii = 1 To Max_Groups
            Group_Status(ii).cmbFac_Sw.Items.Add("Auto")
            Group_Status(ii).cmbFac_Sw.Items.Add("Off")
            Group_Status(ii).cmbFac_Sw.Items.Add("M1")
            Group_Status(ii).cmbFac_Sw.Items.Add("M2")

            Group_Status(ii).cmbESI_Sw.Items.Add("None")
            Group_Status(ii).cmbESI_Sw.Items.Add("M3")
            Group_Status(ii).cmbESI_Sw.Items.Add("M4")
            Group_Status(ii).cmbESI_Sw.Items.Add("M5")
        Next

    End Sub

    Private Sub cmbSignType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSignType.SelectedIndexChanged

        Select Case cmbSignType.Text
            Case "ISLUS"
                nudNumSigns.Maximum = 12
            Case "TMS"
                nudNumSigns.Maximum = 4
            Case Else
                nudNumSigns.Maximum = 1
        End Select
        nudNumSigns.Value = 1
        Change_Num_Signs()

    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Select Case runMode
            Case Mode.DoConfig
                myASyncSocket.Stop_TCP()
            Case Mode.DoRun
                Do_Stop()
                myASyncSocket.Stop_TCP()
            Case Else
                '   do nothing
        End Select
        Application.Exit()
    End Sub

    Private Sub btnStartStop_Click(sender As Object, e As EventArgs) Handles btnStartStop.Click
        Select Case runMode
            Case Mode.SetTCP
                Do_SetTCP()
            Case Mode.SetSignType
                Do_SetSignType()
            Case Mode.DoConfig
                Do_Start()
            Case Mode.DoRun
                Do_Stop()
        End Select
    End Sub

    '################################################################################################################

    Private Sub tmrLoop_Tick(sender As Object, e As EventArgs) Handles tmrLoop.Tick
        If Rx_Comms(0).Num_Bytes <> 0 Then
            If runMode = Mode.DoRun Then
                Rx_Tx.Rx_Packet(Rx_Comms(0))
                Rx_Tx.Tx_Packet(Tx_Comms(0))
                ASynchronousSocketListener.SendPacket(Tx_Comms(0))
            Else
                Rx_Tx.Rx_Flush(Rx_Comms(0))
            End If
        End If
    End Sub

    '################################################################################################################

    Private Sub Do_SetTCP()
        runMode = Mode.SetSignType
        btnStartStop.Text = "Set Sign Type"
        lblTCPPort.Enabled = False
        numTCPPort.Enabled = False
        lblSignType.Enabled = True
        cmbSignType.Enabled = True
    End Sub

    Private Sub Do_SetSignType()
        myASyncSocket = New ASynchronousSocketListener
        If myASyncSocket.Start_TCP(numTCPPort.Value) Then
            lblSignType.Enabled = False
            cmbSignType.Enabled = False
            runMode = Mode.DoConfig
            btnStartStop.Text = "Start"
            Change_Num_Signs()
            Change_Num_Groups()
            Enable_Fields(True)
            Set_Ctrl_Enabled_SignType()
            If Rx_Comms(0).Num_Bytes <> 0 Then
                Rx_Tx.Rx_Flush(Rx_Comms(0))
            End If
            tmrLoop.Enabled = True
        Else
            MsgBox("Cannot initialise interface", vbOKOnly + vbCritical, "Sign_Tester")
            Application.Exit()
        End If
    End Sub

    Private Sub Do_Stop()
        runMode = Mode.DoConfig
        btnStartStop.Text = "Start"
        Set_Ctrl_Enabled_Config()
        Log_It("Stopped")
    End Sub

    Private Sub Do_Start()

        '   variables
        Dim ii As Integer

        chkDoOutput.Checked = True
        txtProtocol.Text = ""
        Log_It("Started")

        Sign_Status(0).Error_Code = 0
        Sign_Status(0).Prev_Error = 0
        Sign_Status(0).cmbError.SelectedIndex = 0
        Sign_Status(0).Enabled = False

        chkCtrl_OnLine.Checked = False

        runMode = Mode.DoRun
        btnStartStop.Text = "Stop"

        Set_Ctrl_Enabled_Run()

        For ii = 1 To Max_Signs
            Sign_Status(ii).Enabled = True
            Sign_Status(ii).chkEnable.Checked = True

            Sign_Status(ii).Error_Code = 0
            Sign_Status(ii).Prev_Error = 0
            Sign_Status(ii).cmbError.SelectedIndex = 0

            Sign_Status(ii).Frame_ID = 0
            Sign_Status(ii).lblFrame.Text = "0"

            Sign_Status(ii).Message_ID = 0
            Sign_Status(ii).lblMessage.Text = "0"
        Next

        For ii = 1 To 255
            Controller_Status(ii).Frame_Loaded = False
            Controller_Status(ii).Frame_Text = ""
            Controller_Status(ii).Message_Loaded = False
            For jj = 0 To 6
                Controller_Status(ii).Message_Frame_ID(jj) = 0
            Next
        Next

        If cmbSignType.Text = "ISLUS" Then
            For ii = 10 To 110 Step 10
                Set_Speed_Frames(ii)
            Next
            Set_ISLUS_Frame(182, 0, "182 - Exit Left")
            Set_ISLUS_Frame(183, 0, "183 - Exit Right")
            Set_ISLUS_Frame(184, 0, "184 - Merge Left")
            Set_ISLUS_Frame(185, 0, "185 - Merge Right")
            Set_ISLUS_Frame(189, 0, "189 - Red Cross")
            Set_ISLUS_Frame(250, 0, "250 - Matrix Test")
            Set_ISLUS_Frame(251, &H8, "251 - Matrix Test + Annulus")
        End If

        If cmbSignType.Text = "CMS" Then
            Set_CMS_Frames("A", 0)
            Set_CMS_Frames("B", 10)
            Set_CMS_Frames("C", 20)
        End If

        For ii = 1 To Max_Groups
            Group_Status(ii).Fac_Sw = -1
            Group_Status(ii).ESI_Sw = -1
            Group_Status(ii).cmbFac_Sw.SelectedIndex = 0
            Group_Status(ii).cmbESI_Sw.SelectedIndex = 0
        Next

    End Sub

    Private Sub Set_CMS_Frames(Side As String, FirstID As Integer)
        Set_CMS_Frame(FirstID, 0, "Off - Side " & Side)
        Set_CMS_Frame(FirstID, 1, "UD - Side " & Side)
        Set_CMS_Frame(FirstID, 2, "LR - Side " & Side)
        Set_CMS_Frame(FirstID, 3, "WW - Side " & Side)
        Set_CMS_Frame(FirstID, 4, "BF - Side " & Side)
        Set_CMS_Frame(FirstID, 5, "BO - Side " & Side)
        Set_CMS_Frame(FirstID, 6, "Off - Side " & Side)
    End Sub

    Private Sub Set_CMS_Frame(FrameID As Integer, Beacon As Integer, Text As String)
        Dim CRC As CRC_CCITT16 = New CRC_CCITT16

        FrameID += Beacon
        Set_Frame(FrameID, FrameID & " - " & Text)

        Controller_Status(FrameID).Frame_Packet.Clear_Packet()
        CRC.Clear_CRC()
        CRC.Do_CRC(PKT_0B_SET_GRAPHIC_FRAME)
        Controller_Status(FrameID).Frame_Packet.Set_Mi_Code(PKT_0B_SET_GRAPHIC_FRAME)

        Add_Byte_CRC(Controller_Status(FrameID).Frame_Packet, CRC, FrameID)             '   frame id
        Add_Byte_CRC(Controller_Status(FrameID).Frame_Packet, CRC, 0)                   '   revision
        Add_Byte_CRC(Controller_Status(FrameID).Frame_Packet, CRC, 1)                   '   pixel rows
        Add_Byte_CRC(Controller_Status(FrameID).Frame_Packet, CRC, 1)                   '   pixel columns
        Add_Byte_CRC(Controller_Status(FrameID).Frame_Packet, CRC, 0)                   '   colour
        Add_Byte_CRC(Controller_Status(FrameID).Frame_Packet, CRC, Beacon)              '   conspicuity
        Add_Byte_CRC(Controller_Status(FrameID).Frame_Packet, CRC, 1)                   '   length (hi byte)
        Add_Byte_CRC(Controller_Status(FrameID).Frame_Packet, CRC, 0)                   '   length (lo byte)
        Add_Byte_CRC(Controller_Status(FrameID).Frame_Packet, CRC, &H5A)                '   data
        Sign_Tester.Controller_Status(FrameID).Frame_Packet.Add_Byte(CRC.Get_CRC_Hi())  '   CRC
        Sign_Tester.Controller_Status(FrameID).Frame_Packet.Add_Byte(CRC.Get_CRC_Lo())  '   CRC
    End Sub

    Private Sub Set_Speed_Frames(Speed As Integer)
        Set_ISLUS_Frame(Speed, &H8, Speed & " - " & Speed & " + Annulus")
        Set_ISLUS_Frame(Speed + 1, &H10, (Speed + 1) & " - " & Speed & " + Annulus Flash")
    End Sub

    Private Sub Set_ISLUS_Frame(FrameId As Integer, Beacon As Integer, Text As String)
        Dim CRC As CRC_CCITT16 = New CRC_CCITT16

        Set_Frame(FrameId, Text)

        Controller_Status(FrameId).Frame_Packet.Clear_Packet()
        CRC.Clear_CRC()
        CRC.Do_CRC(PKT_0B_SET_GRAPHIC_FRAME)
        Controller_Status(FrameId).Frame_Packet.Set_Mi_Code(PKT_0B_SET_GRAPHIC_FRAME)

        Add_Byte_CRC(Controller_Status(FrameId).Frame_Packet, CRC, FrameId)             '   frame id
        Add_Byte_CRC(Controller_Status(FrameId).Frame_Packet, CRC, 0)                   '   revision
        Add_Byte_CRC(Controller_Status(FrameId).Frame_Packet, CRC, 1)                   '   pixel rows
        Add_Byte_CRC(Controller_Status(FrameId).Frame_Packet, CRC, 1)                   '   pixel columns
        Add_Byte_CRC(Controller_Status(FrameId).Frame_Packet, CRC, 0)                   '   colour
        Add_Byte_CRC(Controller_Status(FrameId).Frame_Packet, CRC, Beacon)              '   conspicuity
        Add_Byte_CRC(Controller_Status(FrameId).Frame_Packet, CRC, 1)                   '   length (hi byte)
        Add_Byte_CRC(Controller_Status(FrameId).Frame_Packet, CRC, 0)                   '   length (lo byte)
        Add_Byte_CRC(Controller_Status(FrameId).Frame_Packet, CRC, &H5A)                '   data
        Sign_Tester.Controller_Status(FrameId).Frame_Packet.Add_Byte(CRC.Get_CRC_Hi())  '   CRC
        Sign_Tester.Controller_Status(FrameId).Frame_Packet.Add_Byte(CRC.Get_CRC_Lo())  '   CRC
    End Sub

    Private Sub Set_Frame(Id As Integer, Text As String)
        Controller_Status(Id).Frame_Loaded = True
        Controller_Status(Id).Frame_Text = Text
    End Sub

    Private Sub Set_Ctrl_Enabled_TCP()
        '   local variable
        Dim ii As Integer

        cmbSignType.Enabled = False
        nudNumSigns.Enabled = False
        cmbCtrlErr.Enabled = False

        '   now the Sign_Data class
        For ii = 1 To Max_Signs
            Sign_Status(ii).cmbError.Enabled = False
            Sign_Status(ii).nudGroupID.Enabled = False
        Next

        '   now the Sign_Data class
        For ii = 1 To Max_Groups
            Group_Status(ii).cmbFac_Sw.Enabled = False
            Group_Status(ii).cmbESI_Sw.Enabled = False
        Next
    End Sub

    Private Sub Set_Ctrl_Enabled_SignType()
        '   local variable
        Dim ii As Integer

        cmbSignType.Enabled = False
        nudNumSigns.Enabled = True
        cmbCtrlErr.Enabled = False

        '   now the Sign_Data class
        For ii = 1 To Max_Signs
            Sign_Status(ii).cmbError.Enabled = False
            Sign_Status(ii).chkEnable.Enabled = False
            Sign_Status(ii).nudGroupID.Enabled = False
        Next

        '   now the Sign_Data class
        For ii = 1 To Max_Groups
            Group_Status(ii).cmbFac_Sw.Enabled = False
            Group_Status(ii).cmbESI_Sw.Enabled = False
        Next
    End Sub

    Private Sub Set_Ctrl_Enabled_Config()
        '   local variable
        Dim ii As Integer

        nudNumSigns.Enabled = True
        cmbCtrlErr.Enabled = False

        '   now the Sign_Data class
        For ii = 1 To Max_Signs
            Sign_Status(ii).cmbError.Enabled = False
            Sign_Status(ii).chkEnable.Enabled = False
            Sign_Status(ii).nudGroupID.Enabled = True
        Next

        '   now the Sign_Data class
        For ii = 1 To Max_Groups
            Group_Status(ii).cmbFac_Sw.Enabled = False
            Group_Status(ii).cmbESI_Sw.Enabled = False
        Next
    End Sub

    Private Sub Set_Ctrl_Enabled_Run()
        '   local variable
        Dim ii As Integer

        nudNumSigns.Enabled = False
        cmbCtrlErr.Enabled = True

        '   now the Sign_Data class
        For ii = 1 To Max_Signs
            Sign_Status(ii).cmbError.Enabled = True
            Sign_Status(ii).chkEnable.Enabled = True
            Sign_Status(ii).nudGroupID.Enabled = False
        Next

        '   now the Sign_Data class
        For ii = 1 To Max_Groups
            Group_Status(ii).cmbFac_Sw.Enabled = True
            Group_Status(ii).cmbESI_Sw.Enabled = True
        Next

    End Sub

    Private Sub nudNumSigns_ValueChanged(sender As Object, e As EventArgs) Handles nudNumSigns.ValueChanged
        Change_Num_Signs()
    End Sub

    Public Sub Change_Num_Signs()

        '   local variables
        Dim ii As Integer

        If runMode = Mode.SetTCP Or runMode = Mode.DoConfig Then

            For ii = 1 To Max_Signs
                Sign_Status(ii).Enabled = True
                Sign_Status(ii).chkEnable.Checked = True
                Sign_Status(ii).nudGroupID.Maximum = Math.Min(nudNumSigns.Value, Max_Groups)
                If (ii <= nudNumSigns.Value) Then

                    Do_Visible(Sign_Status(ii), True)

                    If cmbSignType.Text = "TMS" Then
                        Sign_Status(ii).nudGroupID.Value = ii
                        Sign_Status(ii).nudGroupID.Enabled = False
                    Else
                        Sign_Status(ii).nudGroupID.Value = 1
                        Sign_Status(ii).nudGroupID.Enabled = True
                    End If

                Else

                    Do_Visible(Sign_Status(ii), False)

                End If
            Next

        End If

    End Sub

    Public Sub Change_Num_Groups()

        '   local variables
        Dim ii As Integer
        Dim jj As Integer
        Dim Flag As Boolean

        If runMode = Mode.DoConfig Then
            For jj = 1 To Max_Groups
                '   determine if Group ID is valid
                Flag = False
                For ii = 1 To nudNumSigns.Value
                    If Sign_Status(ii).nudGroupID.Value = jj Then
                        Flag = True
                    End If
                Next
                '   set group visibility
                Group_Status(jj).grpGroup.Visible = Flag
                Group_Status(jj).cmbFac_Sw.Visible = Flag
                Group_Status(jj).cmbESI_Sw.Visible = Flag
            Next
        End If

    End Sub

    Public Sub Do_Visible(Status As Sign_Data, enable As Boolean)
        Status.nudGroupID.Visible = enable
        Status.lblSign.Visible = enable
        Status.chkEnable.Visible = enable
        Status.cmbError.Visible = enable
        Status.lblFrame.Visible = enable
        Status.lblMessage.Visible = enable
    End Sub

    Public Sub Log_It(ByRef Data As String)

        If chkDoOutput.Checked Then

            If txtProtocol.TextLength > 10000 Then
                txtProtocol.Text = ""
            End If

            If Sign_Tester.myLog(0).Length > 0 Then
                txtProtocol.AppendText(Sign_Tester.myLog(0) & vbCrLf)
                Sign_Tester.myLog(0) = ""
            End If

            txtProtocol.AppendText(Data & vbCrLf)

        Else
            Sign_Tester.myLog(0) = ""
        End If

    End Sub

    '################################################################################################################

    Private Sub nudGroupID1_ValueChanged(sender As Object, e As EventArgs) Handles nudGroupID1.ValueChanged
        Group_ValueChanged(1, nudGroupID1.Value)
    End Sub

    Private Sub nudGroupID2_ValueChanged(sender As Object, e As EventArgs) Handles nudGroupID2.ValueChanged
        Group_ValueChanged(2, nudGroupID2.Value)
    End Sub

    Private Sub nudGroupID3_ValueChanged(sender As Object, e As EventArgs) Handles nudGroupID3.ValueChanged
        Group_ValueChanged(3, nudGroupID3.Value)
    End Sub

    Private Sub nudGroupID4_ValueChanged(sender As Object, e As EventArgs) Handles nudGroupID4.ValueChanged
        Group_ValueChanged(4, nudGroupID4.Value)
    End Sub

    Private Sub nudGroupID5_ValueChanged(sender As Object, e As EventArgs) Handles nudGroupID5.ValueChanged
        Group_ValueChanged(5, nudGroupID5.Value)
    End Sub

    Private Sub nudGroupID6_ValueChanged(sender As Object, e As EventArgs) Handles nudGroupID6.ValueChanged
        Group_ValueChanged(6, nudGroupID6.Value)
    End Sub

    Private Sub nudGroupID7_ValueChanged(sender As Object, e As EventArgs) Handles nudGroupID7.ValueChanged
        Group_ValueChanged(7, nudGroupID7.Value)
    End Sub

    Private Sub nudGroupID8_ValueChanged(sender As Object, e As EventArgs) Handles nudGroupID8.ValueChanged
        Group_ValueChanged(8, nudGroupID8.Value)
    End Sub

    Private Sub nudGroupID9_ValueChanged(sender As Object, e As EventArgs) Handles nudGroupID9.ValueChanged
        Group_ValueChanged(9, nudGroupID9.Value)
    End Sub

    Private Sub nudGroupID10_ValueChanged(sender As Object, e As EventArgs) Handles nudGroupID10.ValueChanged
        Group_ValueChanged(10, nudGroupID10.Value)
    End Sub

    Private Sub nudGroupID11_ValueChanged(sender As Object, e As EventArgs) Handles nudGroupID11.ValueChanged
        Group_ValueChanged(11, nudGroupID11.Value)
    End Sub

    Private Sub nudGroupID12_ValueChanged(sender As Object, e As EventArgs) Handles nudGroupID12.ValueChanged
        Group_ValueChanged(12, nudGroupID12.Value)
    End Sub

    Private Sub Group_ValueChanged(ii As Integer, Value As Integer)
        Sign_Status(ii).Group_ID = Value
        Change_Num_Groups()
    End Sub

    '################################################################################################################

    Private Sub cmbCtrlErr_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCtrlErr.SelectedIndexChanged
        Error_Changed(0, cmbCtrlErr.Text)
    End Sub

    Private Sub cmbSignErr1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSignErr1.SelectedIndexChanged
        Error_Changed(1, cmbSignErr1.Text)
    End Sub

    Private Sub cmbSignErr2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSignErr2.SelectedIndexChanged
        Error_Changed(2, cmbSignErr2.Text)
    End Sub

    Private Sub cmbSignErr3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSignErr3.SelectedIndexChanged
        Error_Changed(3, cmbSignErr3.Text)
    End Sub

    Private Sub cmbSignErr4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSignErr4.SelectedIndexChanged
        Error_Changed(4, cmbSignErr4.Text)
    End Sub

    Private Sub cmbSignErr5_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSignErr5.SelectedIndexChanged
        Error_Changed(5, cmbSignErr5.Text)
    End Sub

    Private Sub cmbSignErr6_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSignErr6.SelectedIndexChanged
        Error_Changed(6, cmbSignErr6.Text)
    End Sub

    Private Sub cmbSignErr7_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSignErr7.SelectedIndexChanged
        Error_Changed(7, cmbSignErr7.Text)
    End Sub

    Private Sub cmbSignErr8_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSignErr8.SelectedIndexChanged
        Error_Changed(8, cmbSignErr8.Text)
    End Sub

    Private Sub cmbSignErr9_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSignErr9.SelectedIndexChanged
        Error_Changed(9, cmbSignErr9.Text)
    End Sub

    Private Sub cmbSignErr10_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSignErr10.SelectedIndexChanged
        Error_Changed(10, cmbSignErr10.Text)
    End Sub

    Private Sub cmbSignErr11_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSignErr11.SelectedIndexChanged
        Error_Changed(11, cmbSignErr11.Text)
    End Sub

    Private Sub cmbSignErr12_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSignErr12.SelectedIndexChanged
        Error_Changed(12, cmbSignErr12.Text)
    End Sub

    Private Sub Error_Changed(ii As Integer, Error_String As String)
        Sign_Status(ii).Prev_Error = Sign_Status(ii).Error_Code
        Sign_Status(ii).Error_Code = (Convert_Hex(0, Error_String) * 16) + Convert_Hex(1, Error_String)
        Sign_Status(ii).Error_Time = Now
    End Sub

    Private Function Convert_Hex(Index As Integer, Error_String As String) As UInteger
        Dim data As Byte

        data = Convert.ToByte(Error_String.Chars(Index))
        If ((data >= &H30) And (data <= &H39)) Then
            Convert_Hex = data - &H30
        Else
            If ((data >= &H41) And (data <= &H46)) Then
                Convert_Hex = data - &H41 + 10
            Else
                Convert_Hex = 0
            End If
        End If
    End Function

    '################################################################################################################

    Private Sub chkCtrl_OnLine_CheckedChanged(sender As Object, e As EventArgs) Handles chkCtrl_OnLine.CheckedChanged
        Enable_CheckChanged(0, chkCtrl_OnLine.Checked)
    End Sub

    Private Sub chkSignEn1_CheckedChanged(sender As Object, e As EventArgs) Handles chkSignEn1.CheckedChanged
        Enable_CheckChanged(1, chkSignEn1.Checked)
    End Sub

    Private Sub chkSignEn2_CheckedChanged(sender As Object, e As EventArgs) Handles chkSignEn2.CheckedChanged
        Enable_CheckChanged(2, chkSignEn2.Checked)
    End Sub

    Private Sub chkSignEn3_CheckedChanged(sender As Object, e As EventArgs) Handles chkSignEn3.CheckedChanged
        Enable_CheckChanged(3, chkSignEn3.Checked)
    End Sub

    Private Sub chkSignEn4_CheckedChanged(sender As Object, e As EventArgs) Handles chkSignEn4.CheckedChanged
        Enable_CheckChanged(4, chkSignEn4.Checked)
    End Sub

    Private Sub chkSignEn5_CheckedChanged(sender As Object, e As EventArgs) Handles chkSignEn5.CheckedChanged
        Enable_CheckChanged(5, chkSignEn5.Checked)
    End Sub

    Private Sub chkSignEn6_CheckedChanged(sender As Object, e As EventArgs) Handles chkSignEn6.CheckedChanged
        Enable_CheckChanged(6, chkSignEn6.Checked)
    End Sub

    Private Sub chkSignEn7_CheckedChanged(sender As Object, e As EventArgs) Handles chkSignEn7.CheckedChanged
        Enable_CheckChanged(7, chkSignEn7.Checked)
    End Sub

    Private Sub chkSignEn8_CheckedChanged(sender As Object, e As EventArgs) Handles chkSignEn8.CheckedChanged
        Enable_CheckChanged(8, chkSignEn8.Checked)
    End Sub

    Private Sub chkSignEn9_CheckedChanged(sender As Object, e As EventArgs) Handles chkSignEn9.CheckedChanged
        Enable_CheckChanged(9, chkSignEn9.Checked)
    End Sub

    Private Sub chkSignEn10_CheckedChanged(sender As Object, e As EventArgs) Handles chkSignEn10.CheckedChanged
        Enable_CheckChanged(10, chkSignEn10.Checked)
    End Sub

    Private Sub chkSignEn11_CheckedChanged(sender As Object, e As EventArgs) Handles chkSignEn11.CheckedChanged
        Enable_CheckChanged(11, chkSignEn11.Checked)
    End Sub

    Private Sub chkSignEn12_CheckedChanged(sender As Object, e As EventArgs) Handles chkSignEn12.CheckedChanged
        Enable_CheckChanged(12, chkSignEn12.Checked)
    End Sub

    Private Sub Enable_CheckChanged(ii As Integer, Value As Boolean)
        Sign_Status(ii).Enabled = Value
    End Sub

    Private Sub chkDoOutput_CheckedChanged(sender As Object, e As EventArgs) Handles chkDoOutput.CheckedChanged
        If chkDoOutput.Checked Then
            txtProtocol.AppendText(" Resumed" & vbCrLf)
        Else
            txtProtocol.AppendText("Output Paused --->")
        End If
    End Sub

    '################################################################################################################

    Private Sub cmbFacSw1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbFacSw1.SelectedIndexChanged
        Do_Fac_Sw_Change(1, cmbFacSw1.Text)
    End Sub

    Private Sub cmbFacSw2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbFacSw2.SelectedIndexChanged
        Do_Fac_Sw_Change(2, cmbFacSw2.Text)
    End Sub

    Private Sub cmbFacSw3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbFacSw3.SelectedIndexChanged
        Do_Fac_Sw_Change(3, cmbFacSw3.Text)
    End Sub

    Private Sub cmbFacSw4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbFacSw4.SelectedIndexChanged
        Do_Fac_Sw_Change(4, cmbFacSw4.Text)
    End Sub

    Private Sub cmbFacSw5_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbFacSw5.SelectedIndexChanged
        Do_Fac_Sw_Change(5, cmbFacSw5.Text)
    End Sub

    Private Sub cmbFacSw6_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbFacSw6.SelectedIndexChanged
        Do_Fac_Sw_Change(6, cmbFacSw6.Text)
    End Sub

    Public Sub Do_Fac_Sw_Change(groupID As Integer, Fac_Sw_String As String)

        Dim ii As Integer
        Dim data As Char

        '   pick up the second character of the ComboBox's text string
        data = Convert.ToChar(Fac_Sw_String.Chars(1))
        Select Case data
            Case Chr(Asc("f"))                      '   second letter of string "Off"
                Group_Status(groupID).Fac_Sw = 0
            Case Chr(Asc("1"))                      '   second letter of string "M1"
                Group_Status(groupID).Fac_Sw = 1
                If Not Controller_Status(1).Message_Loaded Then
                    Group_Status(groupID).Fac_Sw = 0
                    Group_Status(groupID).cmbFac_Sw.SelectedIndex = 1
                End If
            Case Chr(Asc("2"))                      '   second letter of string "M2"
                Group_Status(groupID).Fac_Sw = 2
                If Not Controller_Status(2).Message_Loaded Then
                    Group_Status(groupID).Fac_Sw = 0
                    Group_Status(groupID).cmbFac_Sw.SelectedIndex = 1
                End If
            Case Else
                Group_Status(groupID).Fac_Sw = -1
        End Select

        For ii = 1 To nudNumSigns.Value
            If Sign_Status(ii).Group_ID = groupID Then
                Sign_Status(ii).Frame_ID = 0
                Sign_Status(ii).Set_Frame_ID = 0
                Sign_Status(ii).lblFrame.Text = Controller_Status(0).Frame_Text
                If Group_Status(groupID).Fac_Sw >= 0 Then
                    Sign_Status(ii).Message_ID = Group_Status(groupID).Fac_Sw
                Else
                    Sign_Status(ii).Message_ID = 0
                End If
                Sign_Status(ii).Set_Message_ID = 0
                Sign_Status(ii).Message_Step = 0
                Sign_Status(ii).lblMessage.Text = "0"
            End If
        Next

    End Sub

    '################################################################################################################

    Private Sub cmbESI1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbESI1.SelectedIndexChanged
        Do_ESI_Sw_Change(1, cmbESI1.Text)
    End Sub

    Private Sub cmbESI2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbESI2.SelectedIndexChanged
        Do_ESI_Sw_Change(2, cmbESI2.Text)
    End Sub

    Private Sub cmbESI3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbESI3.SelectedIndexChanged
        Do_ESI_Sw_Change(3, cmbESI3.Text)
    End Sub

    Private Sub cmbESI4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbESI4.SelectedIndexChanged
        Do_ESI_Sw_Change(4, cmbESI4.Text)
    End Sub

    Private Sub cmbESI5_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbESI5.SelectedIndexChanged
        Do_ESI_Sw_Change(5, cmbESI5.Text)
    End Sub

    Private Sub cmbESI6_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbESI6.SelectedIndexChanged
        Do_ESI_Sw_Change(6, cmbESI6.Text)
    End Sub

    Public Sub Do_ESI_Sw_Change(groupID As Integer, ESI_Sw_String As String)

        Dim ii As Integer
        Dim data As Char

        '   pick up the second character of the ComboBox's text string
        data = Convert.ToChar(ESI_Sw_String.Chars(1))
        Select Case data
            Case Chr(Asc("3"))                      '   second letter of string "M3"
                Group_Status(groupID).ESI_Sw = 3
            Case Chr(Asc("4"))                      '   second letter of string "M4"
                Group_Status(groupID).ESI_Sw = 4
            Case Chr(Asc("5"))                      '   second letter of string "M5"
                Group_Status(groupID).ESI_Sw = 5
            Case Else
                Group_Status(groupID).ESI_Sw = -1
        End Select

        If Group_Status(groupID).ESI_Sw >= 0 Then
            If Not Controller_Status(Group_Status(groupID).ESI_Sw).Message_Loaded Then
                Group_Status(groupID).ESI_Sw = -1
                Group_Status(groupID).cmbESI_Sw.SelectedIndex = 0
            End If
        End If

        For ii = 1 To nudNumSigns.Value
            If Sign_Status(ii).Group_ID = groupID Then
                Sign_Status(ii).Frame_ID = 0
                Sign_Status(ii).Set_Frame_ID = 0
                Sign_Status(ii).lblFrame.Text = "0"
                If Group_Status(groupID).ESI_Sw >= 0 Then
                    Sign_Status(ii).Message_ID = Group_Status(groupID).ESI_Sw
                Else
                    Sign_Status(ii).Message_ID = Sign_Status(ii).Set_Message_ID
                End If
                Sign_Status(ii).Message_Step = 0
                Sign_Status(ii).lblMessage.Text = "0"
            End If
        Next

    End Sub

    '################################################################################################################

End Class
