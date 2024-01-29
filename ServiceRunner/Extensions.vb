Imports System
Imports System.Runtime.CompilerServices

Public Module Extensions
  ''' <summary>
  ''' Throws a <see cref="NullReferenceException"/> if Instance is Nothing.
  ''' </summary>
  ''' <typeparam name="T"></typeparam>
  ''' <param name="Instance">The parameter to be tested.</param>
  ''' <param name="ParameterName">?The name of the parameter to be tested.</param>
  ''' <exception cref="NullReferenceException"></exception>
  <Extension>
  Public Sub ThrowIfNothing(Of T As Class)(Instance As T, ParameterName As String)
    If Instance Is Nothing Then
      Throw New NullReferenceException(ParameterName)
    End If
  End Sub
End Module
