(function () {
    var __baseUrl = "/";

    var ErrorCodes = {
        OK: 100,
        SystemError: -99
    }

    var showLoading = function () {
        return ELEMENT.Loading.service({
            lock: true,
            background: 'rgba(0, 0, 0, 0.7)'
        })
    }

    var closeLoading = function (loading) {
        if (loading) {
            loading.close()
        }
    }

    var showMessage = function (message, type) {
        type = type || "success"
        if (message) {
            ELEMENT.Message({
                message: message,
                type: type
            })
        }
    }

    var httpRequest = function (options) {
        var loading = null
        if (typeof options.success !== "function") {
            options.success = function (data) {
            }
        }
        options.dataType = options.dataType || "json";
        // 默认POST为窗体数据
        if (options.type != "GET" || options.type != "DELETE") {
            options.contentType = options.contentType || "application/x-www-form-urlencoded; charset=UTF-8"
        }
        //若为json请求类型则需要序列化
        if (options.contentType == "application/json") {
            options.data = JSON.stringify(options.data)
        }
        jQuery.ajax({
            //请求类型
            type: options.type || "GET",
            dataType: options.dataType,
            contentType: options.contentType,
            async: options.async == undefined ? true : options.async,
            data: options.data || {},
            // 请求地址，若为本地请求则根路径为"/"
            url: (options.local ? "/" : __baseUrl) + options.url,
            beforeSend: function (jqXHR, settings) {
                if (typeof options.beforeSend == "function") {
                    options.beforeSend(jqXHR, settings);
                }
                loading = showLoading();
            },
            complete: function (jqXHR, settings) {
                if (typeof options.complete == "function") {
                    options.complete(jqXHR, settings);
                }
                closeLoading(loading)
            },
            success: function (data, textStatus, jqXHR) {
                //判断返回的是否为JSON，若不为JSON则直接调用回调
                if (options.dataType == "json") {
                    if (data.code == ErrorCodes.OK) {
                        options.success(data);
                    } else {
                        //判断是否设置失败回调
                        if (options.fail) {
                            options.fail(data);
                        }
                        var msg = data.msg || "请求出错请稍后再试";
                        if (data.code == ErrorCodes.SystemError) {
                            msg = "系统出错请稍后再试";
                        }
                        showMessage(msg, "error")
                    }
                } else {
                    options.success(data);
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                //判断是否设置失败回调
                if (options.fail) {
                    options.fail(data);
                }
                if (textStatus == "parsererror") {
                    showMessage("解析结果错误请稍后再试", "error");
                } else {
                    if (errorThrown == "Unauthorized") {
                        location.href = "/login.html"
                    } else {
                        showMessage("网络异常请稍后再试", "error");
                    }
                }
            }
        });
    }

    var __service = function (baseUrl, token) {
        this.baseUrl = baseUrl || "/"
        __baseUrl = this.baseUrl
        this.token = token

        this.login = function (data, success, fail) {
            httpRequest({
                type: "POST",
                local: true,
                contentType: "application/json",
                url: "api/login",
                data: data,
                success: success,
                fail: fail
            })
        };

        this.getPanoList = function (page, pageSize, data, success) {
            httpRequest({
                type: "GET",
                url: "api/frontsite/panos/" + page + "/" + pageSize,
                data: data,
                success: success
            })
        };

        this.getPanoMapList = function (data, success) {
            httpRequest({
                type: "GET",
                url: "api/frontsite/panomaps",
                data: data,
                success: success
            })
        };

        this.getUserList = function (page, pageSize, data, success) {
            httpRequest({
                type: "GET",
                url: "api/frontsite/users/" + page + "/" + pageSize,
                data: data,
                success: success
            })
        };

        this.addPanoTask = function (data, success) {
            httpRequest({
                type: "POST",
                contentType: "application/json",
                url: "api/frontsite/users/panotasks",
                data: data,
                success: success
            })
        };

        this.getNewsList = function (page, pageSize, data, success) {
            httpRequest({
                type: "GET",
                url: "api/frontsite/news/" + page + "/" + pageSize,
                data: data,
                success: success
            })
        };

        /********* 作品市场开始 ********/

        this.getMarketPanoList = function (page, pageSize, data, success) {
            httpRequest({
                type: "GET",
                url: "api/frontsite/pmarket/panos/" + page + "/" + pageSize,
                data: data,
                success: success
            })
        };

        this.addMarketCart = function (market_pano_id, success) {
            var self = this
            httpRequest({
                type: "POST",
                contentType: "application/json",
                url: "api/pmarket/user/carts?market_pano_id=" + market_pano_id + "&token=" + self.token,
                success: success
            })
        };

        this.getMarketCartList = function (success) {
            var self = this
            httpRequest({
                type: "GET",
                contentType: "application/json",
                url: "api/pmarket/user/carts?token=" + self.token,
                success: success
            })
        };

        this.deleteManyMarketCart = function (ids, success) {
            var self = this
            httpRequest({
                type: "DELETE",
                url: "api/pmarket/user/carts/" + ids.join(",") + "?token=" + self.token,
                success: success
            })
        };

        this.submitMarketOrder = function (data, success) {
            var self = this
            httpRequest({
                type: "POST",
                contentType: "application/json",
                url: "api/pmarket/user/panoorders?token=" + self.token,
                data: data,
                success: success
            })
        };

        this.submitPayOrder = function (data, success) {
            var self = this
            httpRequest({
                type: "POST",
                url: "api/payorder/create?token=" + self.token,
                data: data,
                success: success
            })
        };

        /********* 作品市场结束 ********/
    }

    window.Service = {
        install: function (Vue, options) {
            Vue.mixin({
                created: function () {
                    if (!this.$service) {
                        this.$service = new __service(options.baseUrl || "", options.token || "");
                    }
                }
            })
        }
    }
})(jQuery);