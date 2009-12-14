
$(document).ready(function () {
	$('a.deletelink').click(function () {
		if (confirm('Are you sure')) {
			$(this).parents('form').submit();
		}
		return false;
	});
});