<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Ef4Poco.Domain.Course>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Available courses
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Available courses</h2>

    <table>
        <tr>
            <th></th>
            <th>
                Id
            </th>
            <th>
                Title
            </th>
            <th>
                Price
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%= Html.ActionLink("Edit", "Edit", new { id=item.Id }) %>
				<% using (Html.BeginForm("Delete", "Courses", new { id = item.Id }, FormMethod.Post)) { %>
					<a class="deletelink" href="#">Delete</a>
				<% } %>
                <%= Html.ActionLink("Schedule", "Schedule", new { courseid=item.Id })%>
            </td>
            <td>
                <%= Html.Encode(item.Id) %>
            </td>
            <td>
                <%= Html.Encode(item.Title) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:C}", item.Price)) %>
            </td>
        </tr>
    
    <% } %>

    </table>

    <p>
        <%= Html.ActionLink("Create New", "Create") %>
    </p>

</asp:Content>

