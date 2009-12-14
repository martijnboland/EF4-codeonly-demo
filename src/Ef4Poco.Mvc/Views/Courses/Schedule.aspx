<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Ef4Poco.Domain.Course>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Schedule for course '<%= Model.Title %>'
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	
	<h2>Schedule for course '<%= Model.Title %>'</h2>
	<table>
        <tr>
            <th></th>
            <th>
                Id
            </th>
            <th>
                Start date
            </th>
            <th>
                End date
            </th>
			<th>
				Contact hours
			</th>
			<th>
				Teacher
			</th>
			<th>
				Location
			</th>
        </tr>
		
	<% foreach (var item in Model.Schedules) { %>
    
        <tr>
            <td>
				<% using (Html.BeginForm("DeleteSchedule", "Courses", new { courseid = Model.Id, id = item.Id }, FormMethod.Post)) { %>
					<a class="deletelink" href="#">Delete</a>
				<% } %>
            </td>
            <td>
                <%= Html.Encode(item.Id) %>
            </td>
            <td>
                <%= String.Format("{0:d}",item.StartDate) %>
            </td>
            <td>
                <%= String.Format("{0:d}", item.EndDate) %>
            </td>
			<td>
				<%= Html.Encode(item.ContactHours) %>
			</td>
			<td>
                <%= Html.Encode(item.Teacher.Name) %>
            </td>
			<td>
                <%= Html.Encode(item.Location) %>
            </td>
        </tr>
    
    <% } %>

    </table>
	
	<p>
        <%= Html.ActionLink("Create New", "CreateSchedule", new { courseid = Model.Id })%>
    </p>
	
	<p>
		<%=Html.ActionLink("Back to List", "Index") %>
	</p>
</asp:Content>
