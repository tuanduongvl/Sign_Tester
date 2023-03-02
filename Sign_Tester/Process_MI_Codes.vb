Module Process_MI_Codes

    '   constants
    Private Const PKT_00_REJECT As UInteger = &H0
    Private Const PKT_01_STAR_ACK As UInteger = &H1
    Private Const PKT_02_START_SESSION As UInteger = &H2
    Private Const PKT_03_PWD_SEED As UInteger = &H3
    Private Const PKT_04_PASSWORD As UInteger = &H4
    Private Const PKT_05_HEARTBEAT As UInteger = &H5
    Private Const PKT_06_STATUS_REPLY As UInteger = &H6
    Private Const PKT_07_END_SESSION As UInteger = &H7
    Private Const PKT_08_SYSTEM_RESET As UInteger = &H8
    Private Const PKT_09_UPDATE_TIME As UInteger = &H9
    Public Const PKT_0A_SET_TEXT_FRAME As UInteger = &HA
    Public Const PKT_0B_SET_GRAPHIC_FRAME As UInteger = &HB
    Private Const PKT_0C_SET_MESSAGE As UInteger = &HC
    Private Const PKT_0D_SET_PLAN As UInteger = &HD
    Private Const PKT_0E_DISPLAY_FRAME As UInteger = &HE
    Private Const PKT_0F_DISPLAY_MESSAGE As UInteger = &HF
    Private Const PKT_10_ENABLE_PLAN As UInteger = &H10
    Private Const PKT_11_DISABLE_PLAN As UInteger = &H11
    Private Const PKT_12_REQ_EN_PLANS As UInteger = &H12
    Private Const PKT_13_REPORT_EN_PLANS As UInteger = &H13
    Private Const PKT_14_SET_DIMMING As UInteger = &H14
    Private Const PKT_15_POWER_OFF_ON As UInteger = &H15
    Private Const PKT_16_DIS_EN_DEVICE As UInteger = &H16
    Private Const PKT_17_REQ_STORED_FMP As UInteger = &H17
    Private Const PKT_18_GET_FAULT_LOG As UInteger = &H18
    Private Const PKT_19_FAULT_LOG As UInteger = &H19
    Private Const PKT_1A_RESET_FAULT_LOG As UInteger = &H1A
    Private Const PKT_1B_GET_EXTEND_STATUS As UInteger = &H1B
    Private Const PKT_1C_EXTENDED_STATUS As UInteger = &H1C
    Private Const PKT_1D_SET_HI_RES_GRAPHIC As UInteger = &H1D
    Private Const PKT_2B_DISPLAY_ATOMIC_FRAMES As UInteger = &H2B

    '   application error codes
    Private Const ERR_APP_00_NONE As UInteger = &H0
    Private Const ERR_APP_01_OFF_LINE As UInteger = &H1
    Private Const ERR_APP_02_CMD_SYNTAX As UInteger = &H2
    Private Const ERR_APP_03_WRONG_LENGTH As UInteger = &H3
    Private Const ERR_APP_04_DATA_CHK_SUM As UInteger = &H4
    Private Const ERR_APP_05_NOT_ASCII As UInteger = &H5
    Private Const ERR_APP_06_FRAME_TOO_BIG As UInteger = &H6
    Private Const ERR_APP_07_UNKNOWN_MI As UInteger = &H7
    Private Const ERR_APP_08_MI_NOT_SUPPORTED As UInteger = &H8
    Private Const ERR_APP_09_POWER_OFF As UInteger = &H9
    Private Const ERR_APP_0A_UNDEFINED_DEV As UInteger = &HA
    Private Const ERR_APP_0B_BAD_FONT As UInteger = &HB
    Private Const ERR_APP_0C_BAD_COLOR As UInteger = &HC
    Private Const ERR_APP_0D_NO_OVERLAY As UInteger = &HD
    Private Const ERR_APP_0E_BAD_DIM_LEVEL As UInteger = &HE
    Private Const ERR_APP_0F_FMP_ACTIVE As UInteger = &HF
    Private Const ERR_APP_10_FACILITY_SW As UInteger = &H10
    Private Const ERR_APP_11_BAD_CONSPIC As UInteger = &H11
    Private Const ERR_APP_12_BAD_TRANSIT_T As UInteger = &H12
    Private Const ERR_APP_13_FMP_UNDEFINED As UInteger = &H13
    Private Const ERR_APP_14_PLAN_NOT_EN As UInteger = &H14
    Private Const ERR_APP_15_PLAN_ENABLED As UInteger = &H15
    Private Const ERR_APP_16_BAD_SIZE As UInteger = &H16
    Private Const ERR_APP_17_FRAME_TOO_SMALL As UInteger = &H17
    Private Const ERR_APP_1E_INVALID_SETTING As UInteger = &H1E
    Private Const ERR_APP_1F_BAD_MISS_SIGNS As UInteger = &H1F
    Private Const ERR_APP_20_UNDER_LOCAL_CTRL As UInteger = &H20
    Private Const ERR_APP_21_NO_INTERLOCK As UInteger = &H21
    Private Const ERR_APP_22_INTERLOCKED As UInteger = &H22

    '   controller error codes
    Private Const ERR_CTRL_00_NONE As UInteger = &H0
    Private Const ERR_CTRL_01_POWER_FAIL As UInteger = &H1
    Private Const ERR_CTRL_02_COMMS_TIMEOUT As UInteger = &H2
    Private Const ERR_CTRL_03_MEMORY_ERR As UInteger = &H3
    Private Const ERR_CTRL_04_BATTERY_FAIL As UInteger = &H4
    Private Const ERR_CTRL_0D_CTRL_RESET As UInteger = &HD
    Private Const ERR_CTRL_0E_BATTERY_LOW As UInteger = &HE
    Private Const ERR_CTRL_14_OVER_TEMP As UInteger = &H14
    Private Const ERR_CTRL_1C_DSP_TIMEOUT As UInteger = &H1C
    Private Const ERR_CTRL_1C_DOOR_OPEN As UInteger = &H24

    '   sign error codes
    Private Const ERR_SIGN_00_NONE As UInteger = &H0
    Private Const ERR_SIGN_03_MEMORY_ERR As UInteger = &H3
    Private Const ERR_SIGN_05_INTERNAL_COMMS As UInteger = &H5
    Private Const ERR_SIGN_06_LAMP_FAIL As UInteger = &H6
    Private Const ERR_SIGN_07_SINGLE_LED_BAD As UInteger = &H7
    Private Const ERR_SIGN_08_MULTI_LED_BAD As UInteger = &H8
    Private Const ERR_SIGN_09_OVER_TEMP_FAN As UInteger = &H9
    Private Const ERR_SIGN_0A_HEATER_FAIL As UInteger = &HA
    Private Const ERR_SIGN_0B_CONSPICUITY As UInteger = &HB
    Private Const ERR_SIGN_0C_LUMINANCE_FAIL As UInteger = &HC
    Private Const ERR_SIGN_0F_POWERED_OFF As UInteger = &HF
    Private Const ERR_GROUP_10_FACILITY_SW As UInteger = &H10
    Private Const ERR_SIGN_11_DSP_DRIVE_FAIL As UInteger = &H11
    Private Const ERR_SIGN_12_WRONG_CODE As UInteger = &H12
    Private Const ERR_SIGN_13_2_LAMPS_FAIL As UInteger = &H13
    Private Const ERR_SIGN_14_OVER_TEMP As UInteger = &H14

    '   variables
    Private Tx_Data As New Packet_Data

    Public Sub Analyse_Rx_Packet(ByRef Rx_Data As Packet_Data, ByRef Tx_Data As Packet_Data)
        Select Case Rx_Data.Get_Mi_Code
            Case PKT_02_START_SESSION
                Reply_To_02_Start_Session(Tx_Data)

            Case PKT_04_PASSWORD
                Reply_To_04_Password(Tx_Data, Rx_Data.Get_Mi_Code)

            Case PKT_05_HEARTBEAT
                Reply_To_05_Heartbeat(Tx_Data)

            Case PKT_07_END_SESSION
                Reply_To_07_End_Session(Tx_Data, Rx_Data.Get_Mi_Code)

            Case PKT_08_SYSTEM_RESET
                Reply_To_08_System_Reset(Tx_Data, Rx_Data.Get_Mi_Code)

            Case PKT_09_UPDATE_TIME
                Reply_To_09_Set_Time(Tx_Data, Rx_Data.Get_Mi_Code)

            Case PKT_0A_SET_TEXT_FRAME
                Reply_To_0A_Text_Frame(Tx_Data, Rx_Data)

            Case PKT_0B_SET_GRAPHIC_FRAME
                Reply_To_0B_Graphic_Frame(Tx_Data, Rx_Data)

            Case PKT_0C_SET_MESSAGE
                Reply_To_0C_Set_Message(Tx_Data, Rx_Data)

            Case PKT_0E_DISPLAY_FRAME
                Reply_To_0E_Display_Frame(Tx_Data, Rx_Data)

            Case PKT_0F_DISPLAY_MESSAGE
                Reply_To_0F_Display_Message(Tx_Data, Rx_Data)

            Case PKT_14_SET_DIMMING
                Reply_To_14_Set_Dimming(Tx_Data, Rx_Data.Get_Mi_Code)

            Case PKT_15_POWER_OFF_ON
                Reply_To_15_Power_Off_On(Tx_Data, Rx_Data.Get_Mi_Code)

            Case PKT_17_REQ_STORED_FMP
                Reply_To_17_Request_Stored_FMP(Tx_Data, Rx_Data)

            Case PKT_18_GET_FAULT_LOG
                Reply_to_18_Get_Fault_Log(Tx_Data)

            Case PKT_1A_RESET_FAULT_LOG
                Reply_to_1A_Reset_Fault_Log(Tx_Data, Rx_Data.Get_Mi_Code)

            Case PKT_1B_GET_EXTEND_STATUS
                Reply_to_1B_Get_Extend_Status(Tx_Data)

            Case PKT_1D_SET_HI_RES_GRAPHIC
                Reply_to_1D_Set_Hi_Res_Graphic(Tx_Data, Rx_Data)

            Case PKT_2B_DISPLAY_ATOMIC_FRAMES
                Reply_to_2B_Display_Atomic_Frames(Tx_Data, Rx_Data)

            Case Else
                Reply_to_Unknown_MI_Code(Tx_Data, Rx_Data.Get_Mi_Code)
        End Select
    End Sub

    Private Sub Reply_To_02_Start_Session(ByRef Tx_Data As Packet_Data)
        Send_03_Password_Seed(Tx_Data)
    End Sub

    Private Sub Reply_To_04_Password(ByRef Tx_Data As Packet_Data, MI_Code As UInteger)
        '   pretend password was correct
        Sign_Tester.Sign_Status(0).Enabled = True
        Sign_Tester.Sign_Status(0).chkEnable.Checked = True
        Send_01_Star_Ack(Tx_Data, MI_Code)
    End Sub

    Private Sub Reply_To_05_Heartbeat(ByRef Tx_Data As Packet_Data)
        Dim ii As Integer
        Dim Frame_Id As Integer
        Dim Message_Id As Integer
        Dim Message_Step As Integer
        For ii = 1 To Sign_Tester.nudNumSigns.Value
            Message_Id = Sign_Tester.Sign_Status(ii).Message_ID
            If Message_Id <> 0 Then
                Message_Step = Sign_Tester.Sign_Status(ii).Message_Step
                Frame_Id = Sign_Tester.Controller_Status(Message_Id).Message_Frame_ID(Message_Step)
                Sign_Tester.Sign_Status(ii).Frame_ID = Frame_Id
                Sign_Tester.Sign_Status(ii).lblFrame.Text = Sign_Tester.Controller_Status(Frame_Id).Frame_Text
                Sign_Tester.Sign_Status(ii).lblMessage.Text = Sign_Tester.Controller_Status(Message_Id).Message_Text
                Sign_Tester.Sign_Status(ii).pictureBox.Image = Sign_Tester.Controller_Status(Frame_Id).Frame_Image
                Message_Step += 1
                If Sign_Tester.Controller_Status(Message_Id).Message_Frame_ID(Message_Step) = 0 Then
                    Message_Step = 0
                End If
                Sign_Tester.Sign_Status(ii).Message_Step = Message_Step
            End If
        Next
        Sign_Tester.Log_It("Heartbeat")
        Send_06_Sign_Status(Tx_Data)
    End Sub

    Private Sub Reply_To_07_End_Session(ByRef Tx_Data As Packet_Data, MI_Code As UInteger)
        '   pretend password was correct
        Sign_Tester.Sign_Status(0).Enabled = False
        Sign_Tester.Sign_Status(0).chkEnable.Checked = False
        Send_01_Star_Ack(Tx_Data, MI_Code)
    End Sub

    Private Sub Reply_To_08_System_Reset(ByRef Tx_Dataas As Packet_Data, Mi_Code As UInteger)
        Send_01_Star_Ack(Tx_Data, Mi_Code)
    End Sub

    Private Sub Reply_To_09_Set_Time(ByRef Tx_Dataas As Packet_Data, Mi_Code As UInteger)
        Send_01_Star_Ack(Tx_Data, Mi_Code)
    End Sub

    Private Sub Reply_To_0A_Text_Frame(ByRef Tx_Data As Packet_Data, ByRef Rx_Data As Packet_Data)
        If (Sign_Tester.cmbSignType.Text = "ISLUS") Or (Sign_Tester.cmbSignType.Text = "CMS") Then
            Send_00_Reject(Tx_Data, Rx_Data.Get_Mi_Code, ERR_APP_08_MI_NOT_SUPPORTED)
        Else
            Dim Frame_Id As Integer
            Dim num_chars As Integer
            Dim ii As Integer
            Dim str As String
            Frame_Id = Rx_Data.Read_Byte(0)
            If Frame_Id <> 0 Then
                Sign_Tester.Controller_Status(Frame_Id).Frame_Loaded = True
                num_chars = Rx_Data.Read_Byte(5)
                str = ""
                For ii = 1 To num_chars
                    str &= Convert.ToChar(Rx_Data.Read_Byte(ii + 5)).ToString()
                Next
                Sign_Tester.Controller_Status(Frame_Id).Frame_Text = Frame_Id & " - " & Convert_Beacon(Rx_Data.Read_Byte(4)) & " - " & str
                Copy_Packet(Sign_Tester.Controller_Status(Frame_Id).Frame_Packet, Rx_Data)
                Sign_Tester.Log_It("Set Frame " & Sign_Tester.Controller_Status(Frame_Id).Frame_Text)
                Send_06_Sign_Status(Tx_Data)
            Else
                Send_00_Reject(Tx_Data, Rx_Data.Get_Mi_Code, ERR_APP_02_CMD_SYNTAX)
            End If
        End If
    End Sub

    Private Sub Reply_To_0B_Graphic_Frame(ByRef Tx_Data As Packet_Data, ByRef Rx_Data As Packet_Data)
        If (Sign_Tester.cmbSignType.Text = "ISLUS") Or (Sign_Tester.cmbSignType.Text = "CMS") Then
            Send_00_Reject(Tx_Data, Rx_Data.Get_Mi_Code, ERR_APP_08_MI_NOT_SUPPORTED)
        Else
            Dim Frame_Id As Integer
            Frame_Id = Rx_Data.Read_Byte(0)
            If Frame_Id <> 0 Then
                Sign_Tester.Controller_Status(Frame_Id).Frame_Loaded = True
                Sign_Tester.Controller_Status(Frame_Id).Frame_Text = Frame_Id & " - " & Convert_Beacon(Rx_Data.Read_Byte(4)) & " - " & " Graphic Frame"
                Copy_Packet(Sign_Tester.Controller_Status(Frame_Id).Frame_Packet, Rx_Data)
                Sign_Tester.Log_It("Set Frame " & Sign_Tester.Controller_Status(Frame_Id).Frame_Text)
                Send_06_Sign_Status(Tx_Data)
            Else
                Send_00_Reject(Tx_Data, Rx_Data.Get_Mi_Code, ERR_APP_02_CMD_SYNTAX)
            End If
        End If
    End Sub

    Private Sub Reply_To_0C_Set_Message(ByRef Tx_Data As Packet_Data, ByRef Rx_Data As Packet_Data)
        If (Sign_Tester.cmbSignType.Text = "ISLUS") Or (Sign_Tester.cmbSignType.Text = "CMS") Then
            Send_00_Reject(Tx_Data, Rx_Data.Get_Mi_Code, ERR_APP_08_MI_NOT_SUPPORTED)
        Else
            Dim Frame_Id As Integer
            Dim Message_Id As Integer
            Dim Valid As Boolean
            Dim ii As Integer
            Dim jj As Integer
            Message_Id = Rx_Data.Read_Byte(0)
            If Message_Id <> 0 Then
                Sign_Tester.Controller_Status(Message_Id).Message_Text = Message_Id & " - Frames"
                For ii = 0 To 6
                    Sign_Tester.Controller_Status(Message_Id).Message_Frame_ID(ii) = 0
                Next
                ii = 0
                jj = 3
                Valid = True
                While jj < (Rx_Data.Get_Size() - 1)
                    Frame_Id = Rx_Data.Read_Byte(jj)
                    If Sign_Tester.Controller_Status(Frame_Id).Frame_Loaded Then
                        Sign_Tester.Controller_Status(Message_Id).Message_Frame_ID(ii) = Frame_Id
                        Sign_Tester.Controller_Status(Message_Id).Message_Text &= " " & Frame_Id
                    Else
                        Valid = False
                    End If
                    ii += 1
                    jj += 2
                End While
                If Valid Then
                    Sign_Tester.Controller_Status(Message_Id).Message_Loaded = True
                    Copy_Packet(Sign_Tester.Controller_Status(Message_Id).Message_Packet, Rx_Data)
                    Sign_Tester.Log_It("Set Message " & Sign_Tester.Controller_Status(Message_Id).Message_Text)
                    Send_06_Sign_Status(Tx_Data)
                Else
                    Dim str As String
                    str = "Rx Set Msg 0C -"
                    For ii = 0 To Rx_Data.Get_Size() - 1
                        str &= " " & Rx_Data.Read_Byte(ii)
                    Next
                    Sign_Tester.Log_It(str)
                    Send_00_Reject(Tx_Data, Rx_Data.Get_Mi_Code, ERR_APP_13_FMP_UNDEFINED)
                End If
            Else
                Send_00_Reject(Tx_Data, Rx_Data.Get_Mi_Code, ERR_APP_02_CMD_SYNTAX)
            End If
        End If
    End Sub

    Private Sub Reply_To_0E_Display_Frame(ByRef Tx_Data As Packet_Data, ByRef Rx_Data As Packet_Data)
        Dim ii As Integer
        Dim Group_Id As Integer
        Dim Frame_Id As Integer
        Frame_Id = Rx_Data.Read_Byte(1)
        If Sign_Tester.Controller_Status(Frame_Id).Frame_Loaded Then
            Group_Id = Rx_Data.Read_Byte(0)
            For ii = 1 To Sign_Tester.nudNumSigns.Value
                If Sign_Tester.Group_Status(Group_Id).Fac_Sw < 0 Then
                    If Sign_Tester.Group_Status(Group_Id).ESI_Sw >= 0 Then
                        Sign_Tester.Sign_Status(ii).Set_Frame_ID = Frame_Id
                        Sign_Tester.Sign_Status(ii).Set_Message_ID = 0
                    ElseIf Sign_Tester.Sign_Status(ii).Group_ID = Group_Id Then

                        Sign_Tester.Sign_Status(ii).Frame_ID = Frame_Id
                            Sign_Tester.Sign_Status(ii).Set_Frame_ID = Frame_Id
                            Sign_Tester.Sign_Status(ii).lblFrame.Text = Sign_Tester.Controller_Status(Frame_Id).Frame_Text
                            Sign_Tester.Sign_Status(ii).pictureBox.Image = Sign_Tester.Controller_Status(Frame_Id).Frame_Image

                            Sign_Tester.Sign_Status(ii).Message_ID = 0
                            Sign_Tester.Sign_Status(ii).Set_Message_ID = 0
                            Sign_Tester.Sign_Status(ii).Message_Step = 0
                            Sign_Tester.Sign_Status(ii).lblMessage.Text = "0"

                    End If
                    End If
            Next
            Send_01_Star_Ack(Tx_Data, Rx_Data.Get_Mi_Code)
        Else
            Send_00_Reject(Tx_Data, Rx_Data.Get_Mi_Code, ERR_APP_13_FMP_UNDEFINED)
        End If
    End Sub

    Private Sub Reply_To_0F_Display_Message(ByRef Tx_Data As Packet_Data, ByRef Rx_Data As Packet_Data)
        Dim ii As Integer
        Dim Group_Id As Integer
        Dim Message_Id As Integer
        Message_Id = Rx_Data.Read_Byte(1)
        If Sign_Tester.Controller_Status(Message_Id).Message_Loaded Then
            Group_Id = Rx_Data.Read_Byte(0)
            For ii = 1 To Sign_Tester.nudNumSigns.Value
                If Sign_Tester.Group_Status(Group_Id).Fac_Sw < 0 Then
                    If Sign_Tester.Group_Status(Group_Id).ESI_Sw >= 0 Then
                        Sign_Tester.Sign_Status(ii).Set_Frame_ID = 0
                        Sign_Tester.Sign_Status(ii).Set_Message_ID = Message_Id
                    Else
                        Sign_Tester.Sign_Status(ii).Frame_ID = 0
                        Sign_Tester.Sign_Status(ii).Set_Frame_ID = 0
                        Sign_Tester.Sign_Status(ii).lblFrame.Text = ""
                        Sign_Tester.Sign_Status(ii).Message_ID = Message_Id
                        Sign_Tester.Sign_Status(ii).Set_Message_ID = Message_Id
                        Sign_Tester.Sign_Status(ii).Message_Step = 0
                        Sign_Tester.Sign_Status(ii).lblMessage.Text = "0"
                    End If
                End If
            Next
            Send_01_Star_Ack(Tx_Data, Rx_Data.Get_Mi_Code)
        Else
            Send_00_Reject(Tx_Data, Rx_Data.Get_Mi_Code, ERR_APP_13_FMP_UNDEFINED)
        End If
    End Sub

    Private Sub Reply_To_14_Set_Dimming(ByRef Tx_Dataas As Packet_Data, Mi_Code As UInteger)
        Send_01_Star_Ack(Tx_Data, Mi_Code)
    End Sub

    Private Sub Reply_To_15_Power_Off_On(ByRef Tx_Dataas As Packet_Data, Mi_Code As UInteger)
        Send_01_Star_Ack(Tx_Data, Mi_Code)
    End Sub

    Private Sub Reply_To_17_Request_Stored_FMP(ByRef Tx_Data As Packet_Data, ByRef Rx_Data As Packet_Data)
        Dim type As Integer
        Dim Id As Integer
        type = Rx_Data.Read_Byte(0)
        Id = Rx_Data.Read_Byte(1)
        Sign_Tester.Log_It("Type=" & type & "     Id=" & Id)
        If Id <> 0 Then
            Select Case type
                Case 0      '   frame
                    Sign_Tester.Log_It("Frame Loaded = " & Sign_Tester.Controller_Status(Id).Frame_Loaded)
                    If Sign_Tester.Controller_Status(Id).Frame_Loaded Then
                        Copy_Packet(Tx_Data, Sign_Tester.Controller_Status(Id).Frame_Packet)
                    Else
                        Send_00_Reject(Tx_Data, Rx_Data.Get_Mi_Code, ERR_APP_13_FMP_UNDEFINED)
                    End If

                Case 1      '   message
                    If Sign_Tester.Controller_Status(Id).Message_Loaded Then
                        Copy_Packet(Tx_Data, Sign_Tester.Controller_Status(Id).Message_Packet)
                    Else
                        Send_00_Reject(Tx_Data, Rx_Data.Get_Mi_Code, ERR_APP_13_FMP_UNDEFINED)
                    End If

                Case Else
                    Send_00_Reject(Tx_Data, Rx_Data.Get_Mi_Code, ERR_APP_02_CMD_SYNTAX)
            End Select
        Else
            Send_00_Reject(Tx_Data, Rx_Data.Get_Mi_Code, ERR_APP_02_CMD_SYNTAX)
        End If
    End Sub

    Private Sub Reply_to_18_Get_Fault_Log(ByRef Tx_Data As Packet_Data)
        Send_19_Fault_Log(Tx_Data)
    End Sub

    Private Sub Reply_to_1A_Reset_Fault_Log(ByRef Tx_Data As Packet_Data, Mi_Code As UInteger)
        Send_01_Star_Ack(Tx_Data, Mi_Code)
    End Sub

    Private Sub Reply_to_1B_Get_Extend_Status(ByRef Tx_Data As Packet_Data)
        Send_1C_Extend_Status(Tx_Data)
    End Sub

    Private Sub Reply_to_1D_Set_Hi_Res_Graphic(ByRef Tx_Data As Packet_Data, ByRef Rx_Data As Packet_Data)
        If (Sign_Tester.cmbSignType.Text = "ISLUS") Or (Sign_Tester.cmbSignType.Text = "CMS") Then
            Send_00_Reject(Tx_Data, Rx_Data.Get_Mi_Code, ERR_APP_08_MI_NOT_SUPPORTED)
        Else
            Dim Frame_Id As Integer
            Frame_Id = Rx_Data.Read_Byte(0)
            If Frame_Id <> 0 Then
                Sign_Tester.Controller_Status(Frame_Id).Frame_Loaded = True
                Sign_Tester.Controller_Status(Frame_Id).Frame_Text = Frame_Id & " - " & Convert_Beacon(Rx_Data.Read_Byte(4)) & " - " & " Hi-Res Graphic Frame"
                Copy_Packet(Sign_Tester.Controller_Status(Frame_Id).Frame_Packet, Rx_Data)
                Sign_Tester.Log_It("Set Frame " & Sign_Tester.Controller_Status(Frame_Id).Frame_Text)
                Send_06_Sign_Status(Tx_Data)
            Else
                Send_00_Reject(Tx_Data, Rx_Data.Get_Mi_Code, ERR_APP_02_CMD_SYNTAX)
            End If
        End If
    End Sub

    Private Sub Reply_to_2B_Display_Atomic_Frames(ByRef Tx_Data As Packet_Data, ByRef Rx_Data As Packet_Data)
        Dim ii As Integer
        Dim Num_Signs As Integer
        Dim groupID As Integer
        Dim Sign_Id As Integer
        Dim Frame_Id As Integer
        Dim Valid As Boolean

        If Sign_Tester.cmbSignType.Text = "ISLUS" Then
            Valid = True
            groupID = Rx_Data.Read_Byte(0)
            Num_Signs = Rx_Data.Read_Byte(1)
            For ii = 1 To Num_Signs
                If Rx_Data.Read_Byte(ii + ii) > Sign_Tester.nudNumSigns.Value Then
                    Valid = False
                End If
                If Not Sign_Tester.Controller_Status(Rx_Data.Read_Byte(ii + ii + 1)).Frame_Loaded Then
                    Valid = False
                End If
            Next
            If Valid Then
                For ii = 1 To Num_Signs
                    Sign_Id = Rx_Data.Read_Byte(ii + ii)
                    Frame_Id = Rx_Data.Read_Byte(ii + ii + 1)
                    If Sign_Tester.Group_Status(groupID).Fac_Sw < 0 Then
                        If Sign_Tester.Group_Status(groupID).ESI_Sw >= 0 Then
                            Sign_Tester.Sign_Status(Sign_Id).Set_Frame_ID = Frame_Id
                            Sign_Tester.Sign_Status(Sign_Id).Set_Message_ID = 0
                        Else
                            Sign_Tester.Sign_Status(Sign_Id).Frame_ID = Frame_Id
                            Sign_Tester.Sign_Status(Sign_Id).Set_Frame_ID = Frame_Id
                            Sign_Tester.Sign_Status(Sign_Id).lblFrame.Text = Sign_Tester.Controller_Status(Frame_Id).Frame_Text
                            Sign_Tester.Sign_Status(Sign_Id).Message_ID = 0
                            Sign_Tester.Sign_Status(Sign_Id).Set_Message_ID = 0
                            Sign_Tester.Sign_Status(Sign_Id).Message_Step = 0
                            Sign_Tester.Sign_Status(Sign_Id).lblMessage.Text = "0"
                        End If
                    End If
                Next
                Send_01_Star_Ack(Tx_Data, Rx_Data.Get_Mi_Code)
            Else
                Send_00_Reject(Tx_Data, Rx_Data.Get_Mi_Code, ERR_APP_13_FMP_UNDEFINED)
            End If
        Else
            Send_00_Reject(Tx_Data, Rx_Data.Get_Mi_Code, ERR_APP_08_MI_NOT_SUPPORTED)
        End If
    End Sub

    Private Sub Reply_to_Unknown_MI_Code(ByRef Tx_Data As Packet_Data, Mi_Code As UInteger)
        Send_00_Reject(Tx_Data, Mi_Code, ERR_APP_07_UNKNOWN_MI)
    End Sub

    Private Function Convert_Beacon(Beacon As Integer) As String
        Dim code As Integer
        Dim str1 As String
        Dim str2 As String
        code = Beacon And &H7
        Select Case code
            Case 1
                str1 = "UD"
            Case 2
                str1 = "LR"
            Case 3
                str1 = "WW"
            Case 4
                str1 = "BF"
            Case 5
                str1 = "BO"
            Case Else
                str1 = ""
        End Select
        code = (Beacon And &H18) \ 8
        Select Case code
            Case 1
                str2 = "AF"
            Case 2
                str2 = "AO"
            Case Else
                str2 = ""
        End Select
        If str1.Length() = 0 And str2.Length = 0 Then
            Convert_Beacon = "Off"
        Else
            If str1.Length() = 0 Or str2.Length = 0 Then
                Convert_Beacon = str1 & str2
            Else
                Convert_Beacon = str1 & "-" & str2
            End If
        End If
    End Function

    Public Sub Send_00_Reject(ByRef Tx_Data As Packet_Data, MI_Code As UInteger, Reject_Error_Code As UInteger)
        Tx_Data.Clear_Packet()
        Tx_Data.Set_Mi_Code(PKT_00_REJECT)
        Tx_Data.Add_Byte(MI_Code)
        Tx_Data.Add_Byte(Reject_Error_Code)
    End Sub

    Public Sub Send_01_Star_Ack(ByRef Tx_Data As Packet_Data, MI_Code As UInteger)
        Tx_Data.Clear_Packet()
        Tx_Data.Set_Mi_Code(PKT_01_STAR_ACK)
        Tx_Data.Add_Byte(MI_Code)
    End Sub

    Public Sub Send_03_Password_Seed(ByRef Tx_Data As Packet_Data)
        Tx_Data.Clear_Packet()
        Tx_Data.Set_Mi_Code(PKT_03_PWD_SEED)
        Tx_Data.Add_Byte(7)
    End Sub

    Public Sub Send_06_Sign_Status(ByRef Tx_Data As Packet_Data)

        '   variables
        Dim ii As Integer
        Dim dt As System.DateTime

        dt = Now

        Tx_Data.Clear_Packet()
        Tx_Data.Set_Mi_Code(PKT_06_STATUS_REPLY)
        Tx_Data.Add_Byte(Sign_Tester.Sign_Status(0).Enabled And &H1)
        Tx_Data.Add_Byte(0)                 '   application error code
        Tx_Data.Add_Byte(dt.Day)            '   day
        Tx_Data.Add_Byte(dt.Month)          '   month
        Tx_Data.Add_Byte(dt.Year \ 256)     '   year
        Tx_Data.Add_Byte(dt.Year Mod 256)   '   year
        Tx_Data.Add_Byte(dt.Hour)           '   hours
        Tx_Data.Add_Byte(dt.Minute)         '   minutes
        Tx_Data.Add_Byte(dt.Second)         '   seconds
        Tx_Data.Add_Byte(&H12)              '   hardware checksum
        Tx_Data.Add_Byte(&H34)              '   hardware checksum
        Tx_Data.Add_Byte(Sign_Tester.Sign_Status(0).Error_Code)

        Tx_Data.Add_Byte(Sign_Tester.nudNumSigns.Value)
        For ii = 1 To Sign_Tester.nudNumSigns.Value
            Tx_Data.Add_Byte(ii)
            Tx_Data.Add_Byte(Sign_Tester.Sign_Status(ii).Error_Code)
            Tx_Data.Add_Byte(Sign_Tester.Sign_Status(ii).Enabled And &H1)
            Tx_Data.Add_Byte(Sign_Tester.Sign_Status(ii).Frame_ID)
            Tx_Data.Add_Byte(0)
            Tx_Data.Add_Byte(Sign_Tester.Sign_Status(ii).Message_ID)
            Tx_Data.Add_Byte(0)
            Tx_Data.Add_Byte(0)         '   plans
            Tx_Data.Add_Byte(0)
        Next

    End Sub

    Public Sub Send_19_Fault_Log(ByRef Tx_Data As Packet_Data)
        Dim ii As Integer
        Dim jj As Integer

        '   count number of entries
        jj = 0
        For ii = 0 To Sign_Tester.nudNumSigns.Value
            If Sign_Tester.Sign_Status(ii).Prev_Error <> 0 Then
                jj += 1
            End If
            If Sign_Tester.Sign_Status(ii).Error_Code <> 0 Then
                jj += 1
            End If
        Next

        '   packet fixed data
        Tx_Data.Clear_Packet()
        Tx_Data.Set_Mi_Code(PKT_19_FAULT_LOG)
        Tx_Data.Add_Byte(jj)

        If jj <> 0 Then

            For ii = 0 To Sign_Tester.nudNumSigns.Value

                If Sign_Tester.Sign_Status(ii).Prev_Error <> 0 Then
                    '   insert previous entries
                    Tx_Data.Add_Byte(ii)                                                    '   controller=0 or sign ID
                    Tx_Data.Add_Byte(jj)                                                    '   fault ID
                    jj += 1
                    Tx_Data.Add_Byte(Sign_Tester.Sign_Status(ii).Error_Time.Day)            '   day
                    Tx_Data.Add_Byte(Sign_Tester.Sign_Status(ii).Error_Time.Month)          '   month
                    Tx_Data.Add_Byte(Sign_Tester.Sign_Status(ii).Error_Time.Year \ 256)     '   year
                    Tx_Data.Add_Byte(Sign_Tester.Sign_Status(ii).Error_Time.Year Mod 256)   '   year
                    Tx_Data.Add_Byte(Sign_Tester.Sign_Status(ii).Error_Time.Hour)           '   hours
                    Tx_Data.Add_Byte(Sign_Tester.Sign_Status(ii).Error_Time.Minute)         '   minutes
                    Tx_Data.Add_Byte(Sign_Tester.Sign_Status(ii).Error_Time.Second)         '   seconds
                    Tx_Data.Add_Byte(Sign_Tester.Sign_Status(ii).Prev_Error)
                    Sign_Tester.Sign_Status(ii).Prev_Error = 0
                    Tx_Data.Add_Byte(0)                                                     '   fault clearance
                End If

                If Sign_Tester.Sign_Status(ii).Error_Code <> 0 Then
                    '   insert current entries
                    Tx_Data.Add_Byte(ii)                                                    '   controller=0 or sign ID
                    Tx_Data.Add_Byte(jj)                                                    '   fault ID
                    jj += 1
                    Tx_Data.Add_Byte(Sign_Tester.Sign_Status(ii).Error_Time.Day)            '   day
                    Tx_Data.Add_Byte(Sign_Tester.Sign_Status(ii).Error_Time.Month)          '   month
                    Tx_Data.Add_Byte(Sign_Tester.Sign_Status(ii).Error_Time.Year \ 256)     '   year
                    Tx_Data.Add_Byte(Sign_Tester.Sign_Status(ii).Error_Time.Year Mod 256)   '   year
                    Tx_Data.Add_Byte(Sign_Tester.Sign_Status(ii).Error_Time.Hour)           '   hours
                    Tx_Data.Add_Byte(Sign_Tester.Sign_Status(ii).Error_Time.Minute)         '   minutes
                    Tx_Data.Add_Byte(Sign_Tester.Sign_Status(ii).Error_Time.Second)         '   seconds
                    Tx_Data.Add_Byte(Sign_Tester.Sign_Status(ii).Error_Code)
                    Tx_Data.Add_Byte(1)                                                     '   fault onset
                End If

            Next
        End If
    End Sub

    Public Sub Send_1C_Extend_Status(ByRef Tx_Data As Packet_Data)

        '   variables
        Dim ii As Integer
        Dim dt As System.DateTime
        Dim CRC As CRC_CCITT16 = New CRC_CCITT16

        dt = Now

        Tx_Data.Clear_Packet()
        Tx_Data.Set_Mi_Code(PKT_1C_EXTENDED_STATUS)
        CRC.Clear_CRC()
        CRC.Do_CRC(PKT_1C_EXTENDED_STATUS)

        Add_Byte_CRC(Tx_Data, CRC, Sign_Tester.Sign_Status(0).Enabled And &H1)
        Add_Byte_CRC(Tx_Data, CRC, 0)                   '   application error code
        Add_Byte_CRC(Tx_Data, CRC, Asc("P"c))           '   manufacturer code
        Add_Byte_CRC(Tx_Data, CRC, Asc("e"c))
        Add_Byte_CRC(Tx_Data, CRC, Asc("t"c))
        Add_Byte_CRC(Tx_Data, CRC, Asc("e"c))
        Add_Byte_CRC(Tx_Data, CRC, Asc("r"c))
        Add_Byte_CRC(Tx_Data, CRC, Asc(" "c))
        Add_Byte_CRC(Tx_Data, CRC, Asc("S"c))
        Add_Byte_CRC(Tx_Data, CRC, Asc(" "c))
        Add_Byte_CRC(Tx_Data, CRC, Asc(" "c))
        Add_Byte_CRC(Tx_Data, CRC, Asc(" "c))
        Add_Byte_CRC(Tx_Data, CRC, dt.Day)              '   day
        Add_Byte_CRC(Tx_Data, CRC, dt.Month)            '   month
        Add_Byte_CRC(Tx_Data, CRC, dt.Year \ 256)       '   year
        Add_Byte_CRC(Tx_Data, CRC, dt.Year Mod 256)     '   year
        Add_Byte_CRC(Tx_Data, CRC, dt.Hour)             '   hours
        Add_Byte_CRC(Tx_Data, CRC, dt.Minute)           '   minutes
        Add_Byte_CRC(Tx_Data, CRC, dt.Second)           '   seconds
        Add_Byte_CRC(Tx_Data, CRC, Sign_Tester.Sign_Status(0).Error_Code)

        Add_Byte_CRC(Tx_Data, CRC, Sign_Tester.nudNumSigns.Value)
        For ii = 1 To Sign_Tester.nudNumSigns.Value
            Add_Byte_CRC(Tx_Data, CRC, ii)
            Add_Byte_CRC(Tx_Data, CRC, 1)               '   graphics
            Add_Byte_CRC(Tx_Data, CRC, 7)               '   rows
            Add_Byte_CRC(Tx_Data, CRC, 124)             '   columns
            Add_Byte_CRC(Tx_Data, CRC, Sign_Tester.Sign_Status(ii).Error_Code)
            Add_Byte_CRC(Tx_Data, CRC, 0)               '   auto dimming
            Add_Byte_CRC(Tx_Data, CRC, 7)               '   dimming level
            Add_Byte_CRC(Tx_Data, CRC, 1)               '   number of LED fault bytes
            Add_Byte_CRC(Tx_Data, CRC, 0)               '   LED fault byte
        Next

        Tx_Data.Add_Byte(CRC.Get_CRC_Hi())              '   CRC
        Tx_Data.Add_Byte(CRC.Get_CRC_Lo())              '   CRC

    End Sub

    Public Sub Add_Byte_CRC(ByRef Tx_Data As Packet_Data, ByRef CRC As CRC_CCITT16, data As Byte)
        Tx_Data.Add_Byte(data)
        CRC.Do_CRC(data)
    End Sub

    Public Sub Copy_Packet(To_PD As Packet_Data, From_PD As Packet_Data)
        Dim ii As Integer
        To_PD.Clear_Packet()
        To_PD.Set_Mi_Code(From_PD.Get_Mi_Code())
        For ii = 0 To From_PD.Get_Size() - 1
            To_PD.Add_Byte(From_PD.Read_Byte(ii))
        Next
    End Sub

End Module
