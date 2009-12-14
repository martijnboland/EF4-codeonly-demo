<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Teacher>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Create teacher
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Create teacher</h2>

    <%= Html.ValidationSummary("Create was unsuccessful. Please correct the errors and try again.") %>
	<% Html.EnableClientValidation(); %>
    <% using (Html.BeginForm()) {%>

        <fieldset>
            <legend>Fields</legend>
			<p>
	            <label for="Name">Name:</label>
				<%= Html.TextBox("Name") %>
				<%= Html.ValidationMessage("Name", "*") %>
			</p>
            <p>
                <input type="submit" value="Create" />
            </p>
        </fieldset>
    <% } %>

    <div>
        <%=Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>

