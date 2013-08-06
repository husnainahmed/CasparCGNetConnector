﻿Public Class CasparCGCommandFactory

    Public Shared Function getLoadbg(ByVal channel As Integer, ByVal layer As Integer, ByVal media As String, Optional ByRef autostarting As Boolean = False, Optional ByVal looping As Boolean = False, Optional ByVal seek As Long = 0, Optional ByVal length As Long = 0, Optional ByVal transition As CasparCGTransition = Nothing, Optional ByVal filter As String = "") As String
        Dim cmd As String = "LOADBG " & getDestination(channel, layer) & " '" & media & "'"

        If looping Then
            cmd = cmd & " LOOP"
        End If
        If Not IsNothing(transition) Then
            cmd = cmd & " " & transition.toString
        End If
        If seek > 0 Then
            cmd = cmd & " SEEK " & seek
        End If
        If length > 0 Then
            cmd = cmd & " LENGTH " & length
        End If
        If autostarting Then
            cmd = cmd & " AUTO"
        End If
        If filter.Length > 0 Then
            cmd = cmd & " FILTER " & filter
        End If

        Return escape(cmd)
    End Function

    Public Shared Function getLoad(ByVal channel As Integer, ByVal layer As Integer, ByVal media As String, Optional ByVal looping As Boolean = False, Optional ByVal seek As Long = 0, Optional ByVal length As Long = 0, Optional ByVal transition As CasparCGTransition = Nothing, Optional ByVal filter As String = "") As String
        Dim cmd As String = "LOAD " & getDestination(channel, layer) & " '" & media & "'"

        If looping Then
            cmd = cmd & " LOOP"
        End If
        If Not IsNothing(transition) Then
            cmd = cmd & " " & transition.toString
        End If
        If seek > 0 Then
            cmd = cmd & " SEEK " & seek
        End If
        If length > 0 Then
            cmd = cmd & " LENGTH " & length
        End If
        If filter.Length > 0 Then
            cmd = cmd & " FILTER " & filter
        End If

        Return escape(cmd)
    End Function

    Public Shared Function getPlay(ByVal channel As Integer, ByVal layer As Integer, Optional ByVal media As String = "", Optional ByVal looping As Boolean = False, Optional ByVal seek As Long = 0, Optional ByVal length As Long = 0, Optional ByVal transition As CasparCGTransition = Nothing, Optional ByVal filter As String = "") As String
        Dim cmd As String = "PLAY " & getDestination(channel, layer)

        If media.Length > 0 Then
            cmd = cmd & " '" & media & "'"
        End If
        If looping Then
            cmd = cmd & " LOOP"
        End If
        If Not IsNothing(transition) Then
            cmd = cmd & " " & transition.toString
        End If
        If seek > 0 Then
            cmd = cmd & " SEEK " & seek
        End If
        If length > 0 Then
            cmd = cmd & " LENGTH " & length
        End If
        If filter.Length > 0 Then
            cmd = cmd & " FILTER " & filter
        End If

        Return escape(cmd)
    End Function

    Public Shared Function getPlay(ByVal channel As Integer, ByVal layer As Integer, ByVal media As CasparCGMedia, Optional ByVal looping As Boolean = False, Optional ByVal fill As Boolean = True, Optional ByVal seek As Long = 0, Optional ByVal length As Long = 0, Optional ByVal transition As CasparCGTransition = Nothing, Optional ByVal filter As String = "") As String
        If IsNothing(media) Then
            Return getPlay(channel, layer, , looping, seek, length, transition, filter)
        Else
            If fill Then
                If seek = 0 AndAlso media.containsInfo("frame-number") AndAlso media.containsInfo("nb-frames") AndAlso media.getInfo("frame-number") < media.getInfo("nb-frames") Then
                    seek = Long.Parse(media.getInfo("frame-number"))
                End If
                If length = 0 AndAlso media.containsInfo("nb-frames") Then
                    length = media.getInfo("nb-frames")
                End If
            End If
            Return getPlay(channel, layer, media.getFullName, looping, seek, length, transition, filter)
        End If
    End Function

    Public Shared Function getCall(ByVal channel As Integer, ByVal layer As Integer, Optional ByVal looping As Boolean = False, Optional ByVal seek As Long = 0, Optional ByVal length As Long = 0, Optional ByVal transition As CasparCGTransition = Nothing, Optional ByVal filter As String = "") As String
        Dim cmd As String = "CALL " & getDestination(channel, layer)

        If looping Then
            cmd = cmd & " LOOP"
        End If
        If Not IsNothing(transition) Then
            cmd = cmd & " " & transition.toString
        End If
        If seek > 0 Then
            cmd = cmd & " SEEK " & seek
        End If
        If length > 0 Then
            cmd = cmd & " LENGTH " & length
        End If
        If filter.Length > 0 Then
            cmd = cmd & " FILTER " & filter
        End If

        Return cmd
    End Function

    Public Shared Function getSwap(ByVal channelA As Integer, ByVal channelB As Integer) As String
        Return "SWAP " & channelA & " " & channelB
    End Function

    Public Shared Function getSwap(ByVal channelA As Integer, ByVal channelB As Integer, ByVal layerA As Integer, ByVal layerB As Integer) As String
        Return "SWAP " & channelA & "-" & layerA & " " & channelB & "-" & layerB
    End Function

    Public Shared Function getStop(ByVal channel As Integer, ByVal layer As Integer) As String
        Return "STOP " & getDestination(channel, layer)
    End Function

    Public Shared Function getPause(ByVal channel As Integer, ByVal layer As Integer) As String
        Return "PAUSE " & getDestination(channel, layer)
    End Function

    Public Shared Function getClear(Optional ByVal channel As Integer = -1, Optional ByVal layer As Integer = -1) As String
        Dim cmd As String = "CLEAR"
        If channel > -1 Then
            cmd = cmd & " " & getDestination(channel, layer)
        End If
        Return cmd
    End Function

    Public Shared Function getInfo(Optional ByVal channel As Integer = -1, Optional ByVal layer As Integer = -1, Optional ByVal onlyBackground As Boolean = False, Optional ByVal onlyForeground As Boolean = False) As String
        Dim cmd As String = "INFO"
        If channel > -1 Then
            cmd = cmd & " " & getDestination(channel, layer)
            If layer > -1 Then
                If onlyBackground Then
                    cmd = cmd & " B"
                ElseIf onlyForeground Then
                    cmd = cmd & " F"
                End If
            End If
        End If
        Return cmd
    End Function

    Public Shared Function getInfo(ByRef template As CasparCGTemplate) As String
        Return escape("INFO TEMPLATE '" & template.getFullName & "'")
    End Function

    Public Shared Function getThumbnail(ByVal media As String) As String
        Return escape("THUMBNAIL RETRIEVE '" & media & "'")
    End Function

    Public Shared Function getThumbnail(ByVal media As CasparCGMedia) As String
        Return escape("THUMBNAIL RETRIEVE '" & media.getFullName & "'")
    End Function

    Public Shared Function getThumbnailGenerate(ByVal media As String) As String
        Return escape("THUMBNAIL GENERATE '" & media & "'")
    End Function

    Public Shared Function getThumbnailGenerate(ByVal media As CasparCGMedia) As String
        Return escape("THUMBNAIL GENERATE '" & media.getFullName & "'")
    End Function

    Public Shared Function getThumbnailList() As String
        Return "THUMBNAIL LIST"
    End Function

    Public Shared Function getVersion(Optional ByVal ofPart As String = "Server") As String
        Return "VERSION " & ofPart
    End Function

    '' CG CMD für Flashtemplates
    Public Shared Function getCGAdd(ByVal channel As Integer, ByVal layer As Integer, ByVal template As CasparCGTemplate, ByVal flashlayer As Integer, Optional ByVal playOnLoad As Boolean = False) As String
        Dim cmd As String = "CG " & getDestination(channel, layer) & " ADD " & flashlayer & " " & template.getFullName

        If playOnLoad Then
            cmd = cmd & " 1 "
        Else
            cmd = cmd & " 0 "
        End If
        cmd = cmd & template.getData.toXML
        Return escape(cmd)
    End Function

    Public Shared Function getCGRemove(ByVal channel As Integer, ByVal layer As Integer, ByVal flashlayer As Integer) As String
        Return "CG " & getDestination(channel, layer) & " REMOVE " & flashlayer
    End Function

    Public Shared Function getCGPlay(ByVal channel As Integer, ByVal layer As Integer, ByVal flashlayer As Integer) As String
        Return "CG " & getDestination(channel, layer) & " PLAY " & flashlayer
    End Function

    Public Shared Function getCGStop(ByVal channel As Integer, ByVal layer As Integer, ByVal flashlayer As Integer) As String
        Return "CG " & getDestination(channel, layer) & " STOP " & flashlayer
    End Function

    Public Shared Function getCGNext(ByVal channel As Integer, ByVal layer As Integer, ByVal flashlayer As Integer) As String
        Return "CG " & getDestination(channel, layer) & " NEXT " & flashlayer
    End Function

    Public Shared Function getCGUpdate(ByVal channel As Integer, ByVal layer As Integer, ByVal flashlayer As Integer, ByRef data As CasparCGTemplateData) As String
        Return "CG " & getDestination(channel, layer) & " UPDATE " & flashlayer & " " & escape(data.toXML)
    End Function

    Public Shared Function getCGInvoke(ByVal channel As Integer, ByVal layer As Integer, ByVal flashlayer As Integer, ByVal method As String) As String
        Return "CG " & getDestination(channel, layer) & " INVOKE " & flashlayer & " " & method
    End Function

    Public Shared Function getCGClear(ByVal channel As Integer, ByVal layer As Integer) As String
        Return "CG " & getDestination(channel, layer) & " CLEAR"
    End Function

    Private Shared Function getDestination(ByVal channel As Integer, ByVal layer As Integer) As String
        Dim cmd As String
        If channel > -1 Then
            cmd = channel
        Else
            cmd = "0"
        End If
        If layer > -1 Then
            cmd = cmd & "-" & layer
        End If
        Return cmd
    End Function

    Public Shared Function getCls() As String
        Return "CLS"
    End Function

    Public Shared Function getTls() As String
        Return "TLS"
    End Function


    ''' <summary>
    ''' Escapes the string str as needed for casparCG Server
    ''' </summary>
    ''' <param name="str"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function escape(ByVal str As String) As String
        ' Backslash
        str = str.Replace("\", "\\")

        ' Hochkommata
        str = str.Replace("'", "\'")
        str = str.Replace("""", "\""")
        Return str
    End Function

End Class
