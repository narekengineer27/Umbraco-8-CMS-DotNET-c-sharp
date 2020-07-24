$(function () {
    'use strict';

    var idleTime = 0;
    var maxAllowedIdleTime = 60;
    var tokenRefreshInterval = 30;

    function timerIncrement() {
        idleTime = idleTime + 1;
    }

    $(document).ready(function () {
        //Increment the idle time counter every minute.
        var idleInterval = setInterval(timerIncrement, 60 * 1000); // 1 minute

        //Zero the idle timer on mouse movement.
        $(this).mousemove(function (e) {
            idleTime = 0;
        });
        $(this).keypress(function (e) {
            idleTime = 0;
        });
		
        const urlParams = new URLSearchParams(window.location.search);
        if (urlParams.get('referrer') != null && document.getElementById("popups") != null) {
            document.getElementById("popups").click();
        }
    });
	
    if (Cookies.get('referrer') === undefined || Cookies.get('referrer') === 'null') {
        const urlParams = new URLSearchParams(window.location.search);
        const referrer = urlParams.get('referrer');
        if (referrer != null) {
            Cookies.set('referrer', referrer, { path: '/' });
        }
    }

    if (Cookies.get('username') !== undefined) {

        var date = new Date();
        var lastLogin = new Date(parseInt(Cookies.get('lastLogin').substr(6)));
        var lastLoginUtc = new Date(lastLogin.getUTCFullYear(), lastLogin.getUTCMonth(), lastLogin.getUTCDate(), lastLogin.getUTCHours(), lastLogin.getUTCMinutes(), lastLogin.getUTCSeconds());
        var loginInfo = {
            "username": Cookies.get('username'),
            "customerToken": Cookies.get('token'),
            "lastLogin": lastLoginUtc,
            "tenantUid": $('#tenantUid-root').val(),
            "nowUtc": new Date(date.getUTCFullYear(), date.getUTCMonth(), date.getUTCDate(), date.getUTCHours(), date.getUTCMinutes(), date.getUTCSeconds())
        };

        if (loginInfo.lastLogin !== undefined) {
            var expiresAt = new Date(lastLogin.getUTCFullYear(), lastLogin.getUTCMonth(), lastLogin.getUTCDate(), lastLogin.getUTCHours(), lastLogin.getUTCMinutes() + tokenRefreshInterval, lastLogin.getUTCSeconds());
            var countdown = Math.floor((expiresAt - loginInfo.nowUtc) / 1000);
            setInterval(function () {
                countdown--;
                if (countdown <= 0) {
                    if (idleTime <= maxAllowedIdleTime) {
                        var data = {
                            "token": loginInfo.customerToken,
                            "tenantUid": loginInfo.tenantUid
                        };
                        $.post("/umbraco/surface/account/refreshtoken",
                            data,
                            function (response) {
                                //console.log(response);
                                if (response.Success) {

                                    Cookies.set('username', response.Credential, { expires: response.Expires, path: '/' });
                                    Cookies.set('token', response.Token, { expires: response.Expires, path: '/' });
                                    Cookies.set('lastLogin', response.LastLogin, { expires: response.Expires, path: '/' });

                                    countdown = response.Expires * 60;
                                }
                                else {
                                    console.log("refresh failed");
                                    $('#logout-button').click(); // refresh failed
                                }
                            });
                    }
                    else {
                        $('#logout-button').click(); // logout if idle for over maxAllowedIdleTime
                    }
                }
            }, 1 * 10000);
        }
        else {
            $('#logout-button').click();
        }
    }

    $('#login-form').submit(function (e) {
        e.preventDefault();
        var self = this;
        processing(true);
        var submitButton = $(this).find('input[type=submit]');
        submitButton.attr('disabled', 'disabled');
        $.post("/umbraco/surface/account/login",
            $(this).serialize(),
            function (data) {
                //console.log(data);
                if (data.Success) {
                    // go to next step
                    hideServerError(self);
                    if (data.ValidationCode === 100) {
                        $(self).find('.response-success').show();
                        $(self).find('.response-failure').hide();
                        processing(false);

                        document.username = data.Credential;
                        document.token = data.Token;
                        document.lastLogin = data.LastLogin;
                        document.expires = data.Expires;

                        $(self).find('.response-success').hide();
                        submitButton.removeAttr('disabled');
                        $('#mobile-verification').modal();
                        return;
                    }

                    //document.cookie = 'username=' + data.Credential + '; expires=' + data.Expires + '; path=/';
                    if (data.Expires > 0) {

                        Cookies.set('username', data.Credential, { expires: data.Expires, path: '/' });
                        Cookies.set('token', data.Token, { expires: data.Expires, path: '/' });
                        Cookies.set('lastLogin', data.LastLogin, { expires: data.Expires, path: '/' });
                    }
                    else {
                        Cookies.set('username', data.Credential, { path: '/' });
                        Cookies.set('token', data.Token, { path: '/' });
                        Cookies.set('lastLogin', data.LastLogin, { path: '/' });
                    }
                    $(self).find('.response-success').show();
                    $(self).find('.response-failure').hide();
                    //setTimeout(function () {
                    document.location.reload(true);
                    //}, 1 * 1000);

                    processing(false);
                }
                else {
                    if (data.Errors !== undefined) {
                        showServerError(self, data.Errors);
                    }
                    var error = $(self).find('.response-failure').html();
                    $('.server-error').append(`<br/>${error}`);
                    //console.log(data.Message);

                    //$(self).find('.response-failure').show();
                    enableButton(submitButton);
                    processing(false);
                    //loginWaitTimer();
                }
            }).fail(function (data) {
                //console.log(data);
                enableButton(submitButton);
                processing(false);
                if (data.Errors !== undefined) {
                    showServerError(self, data.Errors);
                }
                $(self).find('.response-failure').show();
            });
    });


    $('#login-formPage').submit(function (e) {
        e.preventDefault();
        var self = this;
        processing(true);
        var submitButton = $(this).find('input[type=submit]');
        submitButton.attr('disabled', 'disabled');
        $.post("/umbraco/surface/account/LoginForm",
            $(this).serialize(),
            function (data) {
                //console.log(data);
                if (data.Success) {
                    // go to next step
                    hideServerError(self);
                    if (data.ValidationCode === 100) {
                        $(self).find('.response-success').show();
                        $(self).find('.response-failure').hide();
                        processing(false);

                        document.username = data.Credential;
                        document.token = data.Token;
                        document.lastLogin = data.LastLogin;
                        document.expires = data.Expires;

                        $(self).find('.response-success').hide();
                        submitButton.removeAttr('disabled');
                        $('#mobile-verification').modal();
                        return;
                    }

                    //document.cookie = 'username=' + data.Credential + '; expires=' + data.Expires + '; path=/';
                    if (data.Expires > 0) {

                        Cookies.set('username', data.Credential, { expires: data.Expires, path: '/' });
                        Cookies.set('token', data.Token, { expires: data.Expires, path: '/' });
                        Cookies.set('lastLogin', data.LastLogin, { expires: data.Expires, path: '/' });
                    }
                    else {
                        Cookies.set('username', data.Credential, { path: '/' });
                        Cookies.set('token', data.Token, { path: '/' });
                        Cookies.set('lastLogin', data.LastLogin, { path: '/' });
                    }
                    $(self).find('.response-success').show();
                    $(self).find('.response-failure').hide();
                    //setTimeout(function () {
                    document.location.reload(true);
                    //}, 1 * 1000);

                    processing(false);
                }
                else {
                    if (data.Errors !== undefined) {
                        showServerError(self, data.Errors);
                    }
                    var error = $(self).find('.response-failure').html();
                    $('.server-error').append(`<br/>${error}`);
                    //console.log(data.Message);

                    //$(self).find('.response-failure').show();
                    enableButton(submitButton);
                    processing(false);
                    //loginWaitTimer();
                }
            }).fail(function (data) {
                //console.log(data);
                enableButton(submitButton);
                processing(false);
                if (data.Errors !== undefined) {
                    showServerError(self, data.Errors);
                }
                $(self).find('.response-failure').show();
            });
    });


    function loginWaitTimer() {
        var waitTime = 60;
        $('.resend-time').show();
        var timer = setInterval(function () {
            waitTime--;
            $('.wait').html(waitTime);
            if (waitTime === 0) {
                clearInterval(timer);
                enableButton($('#login-form').find('input[type=submit]'));
                $('.resend-time').hide();
            }
        }, 1 * 1000);
    }

    $('#logout-button').click(function (e) {
        $(this).attr('disabled', 'disabled');

        var redirectionUrl = "/";
        if (Cookies.get('IsAuthenticatedPage') !== undefined) {
            if (Cookies.get('lang-url') !== undefined) {
                redirectionUrl = Cookies.get('lang-url');
            } else {
                redirectionUrl = "/";
            }
        } else {
            redirectionUrl = window.location.href;
        }

        $.get('/umbraco/surface/account/logout',
            function (data) {
                //console.log(data);
                if (data.Success) {
                    removeAuthCookies();
                    document.location.href = redirectionUrl;
                }
            }).fail(function (data) {
                //console.log(data);
            });
    });

    function removeAuthCookies() {
        Cookies.remove('username', { path: '' });
        Cookies.remove('token', { path: '' });
        Cookies.remove('lastLogin', { path: '' });
    }
});