<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Ef4Poco.Domain.Schedule>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Create Schedule for <%= Model.Course.Title %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Create Schedule for <%= Model.Course.Title %></h2>

    <%= Html.ValidationSummary("Edit was unsuccessful. Please correct the errors and try again.") %>
	<% Html.EnableClientValidation(); %>
    <% using (Html.BeginForm()) {%>

        <fieldset>
            <legend>Fields</legend>
            <p>
                <label for="StartDate">Start date:</label>
                <%= Html.TextBox("StartDate", Model.StartDate > DateTime.MinValue ? String.Format("{0:d}", Model.StartDate) : String.Empty) %>
                <%= Html.ValidationMessage("StartDate", "*") %>
            </p>
            <p>
                <label for="EndDate">End date:</label>
                <%= Html.TextBox("EndDate", Model.EndDate > DateTime.MinValue ? String.Format("{0:d}", Model.EndDate) : String.Empty) %>
                <%= Html.ValidationMessage("EndDate", "*") %>
            </p>
			<p>
                <label for="ContactHours">Contact hours:</label>
                <%= Html.TextBox("ContactHours", Model.ContactHours) %>
                <%= Html.ValidationMessage("ContactHours", "*") %>
            </p>
			<p>
				<label for="teacherid">Teacher:</label>
				<%= Html.DropDownList("teacherid", ViewData["Teachers"] as SelectList) %>
			</p>
            <p>
                <label for="Location">Location:</label>
                <%= Html.TextBox("Location", Model.Location) %>
                <%= Html.ValidationMessage("Location", "*") %>
            </p>
            <p>
                <input type="submit" value="Save" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%=Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>

