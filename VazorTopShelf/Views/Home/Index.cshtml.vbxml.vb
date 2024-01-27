Imports System.Diagnostics.CodeAnalysis
Imports System.Xml.Linq

Namespace Home
  <SuppressMessage("Style", "IDE0047:Remove unnecessary parentheses", Justification:="<Pending>")>
  Partial Public Class IndexView
    Public Overrides Function GetVbXml() As XElement
      ' <zml> is a virtual node and will be deleted. It is only used to
      ' contain all XML nodes in one root, as required by XML Literals.
      ' If your HTML markup is contained in one root node, use it instead.

      Return _
             _
    <zml xmlns:z="zml">
      <z:model type="List(Of VazorTopShelf.Db.Models.Student)"/>

      <h3 fff=""> Browse Students</h3>
      <p>Select from <%= Me.Students.Count() %> students:</p>
      <ul>
        <!--Use lambda expressions to execute vb code block-->
        <%= (Iterator Function()
               For Each std In Me.Students
                 Yield <li><%= std.Name %></li>
               Next
             End Function).Invoke %>
      </ul>
      <!--Or use ZML tags directly-->
      <z:if condition="Model.Count > 1 andalso not (Model.Count >= 10)">
        <p>Students details:</p>
        <ul>
          <z:foreach var="m" in="Model">
            <li>
                        Id: @m.Id<br/>
                        Name: @m.Name<br/>
              <p>Grade: @m.Grade</p>
            </li>
          </z:foreach>
        </ul>
      </z:if>
      <script>
                 var x = 5;
                document.writeln("students count = @Model.Count");
                
        </script>
    </zml>

    End Function
  End Class
End Namespace
