//按钮点击
$("button[type='submit']")
.on("click",
function () {
    if ($(this).attr("data-loading-text")!="undifined") {
        $(this).button('loading').delay(1000).queue(function () {
            $(this).button('reset');
            $(this).dequeue();
        });
    }
});
$("input[type='submit']")
.on("click",
function () {
    if ($(this).attr("data-loading-text") != "undifined") {
        $(this).button('loading').delay(1000).queue(function () {
            $(this).button('reset');
            $(this).dequeue();
        });
    }
});
$(function() {
    //checkbox样式使用icheck优化
    $('input[type="checkbox"].flat-red, input[type="radio"].flat-red').iCheck({
        checkboxClass: 'icheckbox_flat-green',
        radioClass: 'iradio_flat-green'
    });
});

