﻿'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'' Author: Christopher Diekkamp
'' Email: christopher@development.diekkamp.de
'' GitHub: https://github.com/mcdikki
'' 
'' This software is licensed under the 
'' GNU General Public License Version 3 (GPLv3).
'' See http://www.gnu.org/licenses/gpl-3.0-standalone.html 
'' for a copy of the license.
''
'' You are free to copy, use and modify this software.
'' Please let me know of any changes and improvements you made to it.
''
'' Thank you!
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Public Class ThumbnailGenerateAllCommand
    Inherits AbstractCommand

    Public Sub New()
        MyBase.New("THUMBNAIL GENERATE_ALL", "Requests the server to (re-)generate thumbnails for all media files")
    End Sub

    Public Overrides Function getCommandString() As String
        Return "THUMBNAIL GENERATE_ALL"
    End Function

    Public Overrides Function getRequiredVersion() As Integer()
        Return {2, 0, 4}
    End Function

    Public Overrides Function getMaxAllowedVersion() As Integer()
        Return {Integer.MaxValue}
    End Function
End Class
