; (function ($) {
    $.fn.extend({
        vcode: function (settings) {
            for (var i = 0; i < this.length; i++) {
                if (!$(this[i]).data("vcode-options")) {
                    var defaults = {
                        alt: "点击更换验证码",             //鼠标提示
                        url: "/Public/ValidateCode",  //验证码生成地址
                        width: 200,                        //图片宽度
                        height: 75,                        //图片高度
                        loadingImageURL: null,             //等待图片
                        loadingSeconds: 1                  //等待图片停留时间
                    };
                    $.extend(defaults, settings);
                    $(this[i]).data("vcode-options", defaults);
                    $(this[i]).attr("alt", $(this[i]).data("vcode-options").alt)
                        .attr("title", $(this[i]).data("vcode-options").alt)
                        .css("cursor", "pointer");
                    $(this[i]).attr("src", "");
                    $(this[i]).bind("loadImage", function () {
                        var options = $(this).data("vcode-options");
                        var loadingTime = 1000;
                        if (options.loadingImageURL) {
                            loadingTime *= options.loadingSeconds;
                            $(this).attr("src", options.loadingImageURL);
                        } else {
                            loadingTime *= 0;
                            $(this).attr("src", "");
                        }
                        setTimeout(function (tag) {
                            var options = $(tag).data("vcode-options");
                            if (options.width != 200 & options.height != 75)
                                $(tag).attr("src", options.url + "?w=" + options.width + "&h=" + options.height + "&rnd=" + Math.random());
                            else
                                $(tag).attr("src", options.url + "?rnd=" + Math.random());
                        }, loadingTime, this);
                    });
                    $(this[i]).click(function () {
                        $(this).trigger("loadImage");
                    });
                }
                $(this).trigger("loadImage");
            }
            return this;
        }
    });
})(jQuery);