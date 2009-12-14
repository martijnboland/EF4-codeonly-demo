<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Ef4Poco.Domain.Course>" %>
<p>
    <label for="Title">Title:</label>
    <%= Html.TextBox("Title") %>
    <%= Html.ValidationMessage("Title", "*") %>
</p>
<p>
    <label for="Price">Price:</label>
    <%= Html.TextBox("Price", String.Format("{0:F2}", Model.Price)) %>
    <%= Html.ValidationMessage("Price", "*") %>
</p>


