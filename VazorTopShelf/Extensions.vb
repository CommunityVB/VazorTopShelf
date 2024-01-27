Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Runtime.CompilerServices
Imports Microsoft.AspNetCore.Mvc.ViewFeatures
Imports Newtonsoft.Json

Public Module Extensions
  ''' <summary>
  ''' Adds the specified key and value to a <see cref="TempDataDictionary"/>.
  ''' </summary>
  ''' <typeparam name="T"></typeparam>
  ''' <param name="Instance">The instance to be extended.</param>
  ''' <param name="Key">The key.</param>
  ''' <param name="Value">The value to serialize and store.</param>
  ''' <remarks>This extension method is useful when Value is of a type that's too complex for the TempData collection.</remarks>
  <Extension>
  Public Sub Put(Of T As Class)(Instance As ITempDataDictionary, Key As String, Value As T)
    Instance(Key) = JsonConvert.SerializeObject(Value)
  End Sub



  ''' <summary>
  ''' Gets the specified key and value from a <see cref="TempDataDictionary"/>.
  ''' </summary>
  ''' <typeparam name="T"></typeparam>
  ''' <param name="Instance">The instance to be extended.</param>
  ''' <param name="Key">The key.</param>
  ''' <returns>The value corresponding to the supplied Key.</returns>
  ''' <remarks>This extension method is useful when Value is of a type that's too complex for the TempData collection.</remarks>
  <Extension>
  Public Function [Get](Of T As Class)(Instance As ITempDataDictionary, Key As String) As T
    Dim oObject As Object
    Dim oValue As T

    oObject = Nothing

    If Instance.TryGetValue(Key, oObject) Then
      oValue = JsonConvert.DeserializeObject(Of T)(oObject)
    Else
      oValue = Nothing
    End If

    Return oValue
  End Function



  ''' <summary>
  ''' Determines whether no elements of a sequence satisfy a condition.
  ''' </summary>
  ''' <typeparam name="T"></typeparam>
  ''' <param name="Instance">The source sequence to be tested.</param>
  ''' <returns><c>True</c> if the source sequence is empty or none of its elements passes the test in the specified predicate; otherwise, <c>False</c>.</returns>
  <Extension>
  Public Function NotAny(Of T)(Instance As IEnumerable(Of T), Predicate As Func(Of T, Boolean)) As Boolean
    Instance.ThrowIfNothing(NameOf(Instance))
    Predicate.ThrowIfNothing(NameOf(Predicate))

    Return Not Instance.Any(Predicate)
  End Function



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
