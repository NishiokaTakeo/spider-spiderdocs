$(function() {
	SetDatePicker();

	$("#btnClear").click(function() {
	    $(".search_field input, .search_field select").each(function() {
            if($(this).prop("type") != "hidden")
            {
	            if($(this).is(':checked'))
	                $(this).prop("checked", false);
                else
	                $(this).val("");
            }
	    });
	});

    $("#btnResetResult").click(function() {
        $("#btnClear").trigger("click");
        $("#btnSearch").trigger("click");
    });
});
