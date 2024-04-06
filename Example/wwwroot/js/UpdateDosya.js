$(document).ready(function () {
    $("#labelId").hover(function () {
        $(this).css('cursor', 'pointer');
    });
    $("#files").change(function () {
        var data = $(this).val();
        if (data != "")
        {
            var array = data.split("\\");
            var fileName = array[array.length - 1];
            $("#labelId").text(fileName);
        }
    });
});