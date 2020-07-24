//= jquery.min.js
//= jquery.magnific-popup.min.js
//= select.js
//= select2.full.js
//= jquery-ui.js
//= jquery.payment.min.js

/*Language Selection*/
if (Cookies.get('lang') !== undefined) {
    var selectedLang = $('.language-selector > option:checked')[0];
    if (selectedLang.dataset) {
        //var cookieLang = $('.language-selector > option[data-language="' + Cookies.get('lang') + '"]')[0];
        var preventReload = $('input[name=preventReload]').val() === "true" ? true : false;
        if (selectedLang.dataset.language !== Cookies.get('lang') && !preventReload) {
            //window.location.href = cookieLang.dataset.link;
            $($('.select-items > div[data-language="' + Cookies.get('lang') + '"]')[0]).click();
        }
        if (preventReload) {
            $('.loader').hide();
        }
    }
}

$(function () {

    if ($('.slider-big').length) {
        $('.slider-big').owlCarousel({
            loop: false,
            margin: 8,
            nav: true,
            autoWidth: true,
            responsive: {
                0: {
                    items: 1,
                    autoWidth: false
                },
                541: {
                    items: 3
                }
            }
        });
    }

    if ($('.slider-small').length) {
        $('.slider-small').owlCarousel({
            loop: false,
            margin: 8,
            nav: true,
            autoWidth: true,
            items: 4,
            responsive: {
                0: {
                    items: 1
                },
                721: {
                    items: 3
                },
                1000: {
                    items: 4
                }
            }
        });
    }
    var selectedLang = $('.language-selector > option:checked')[0];
    if (Cookies.get('lang') === undefined || selectedLang.dataset.language === Cookies.get('lang')) {
        $('.loader').fadeOut();
    }

    if ($('.logo-top__link svg').length) {
        $('.logo-top__link svg').removeAttr("width");
        $('.logo-top__link svg').removeAttr("height");
        $('.logo-top__link svg').attr('viewBox', '0,0,400,138.29321663019695');
        $('.logo-top__link svg').attr('preserveAspectRatio', 'xMidYMid meet');
    }

    //$('a[disabled]').click(function (e) {
    //    e.preventDefault();
    //});
});

var filterDate = function () {
    var minDate = new Date();
    $(".datapicker").datepicker({
        dateFormat: 'mm/dd/yy',
        dateonly: true
    }).val();
    $(".datapicker").attr('autocomplete', 'off');
    $('.cc-number').formatCardNumber();
};


function heightFooter() {
    if ($('iframe').length === 0) {
        var heightFoot = $('.footer').outerHeight();
        $('body').css({ 'padding-bottom': heightFoot });
    }
}
heightFooter();
$(window).resize(function () {
    heightFooter();
});

$(".burger-menu").on("click", function (e) {
    e.preventDefault();
    $(this).toggleClass('is-active');
    $('.top-line').toggleClass('is-active');
    //$('.mobile-menu').toggleClass('is-active');
    $('body').toggleClass('nav-active');
    $("body").toggleClass('overflow');
});
$(document).mouseup(function (e) {
    var container = $(".mobile-menu, .burger-menu");
    if (container.has(e.target).length === 0) {
        $('.burger-menu').removeClass('is-active');
        $('.top-line').removeClass('is-active');
        //$('.mobile-menu').removeClass('is-active');
        $('body').removeClass('nav-active');
        $("body").removeClass('overflow');
    }
});

$('.search-link').on("click", function (e) {
    e.preventDefault();
    $(this).toggleClass('is-active');
    $(this).parents('.head-search__search').toggleClass('no-border');
    $('.search-form--head').toggleClass('is-active');
    setTimeout(() => {
        $('.search-input').focus();
    }, 300);
});

$(document).mouseup(function (e) {
    var container = $(".head-search");
    if (container.has(e.target).length === 0) {
        $('.search-link').removeClass('is-active');
        $('.search-link').parents('.head-search__search').removeClass('no-border');
        $('.search-form--head').removeClass('is-active');
    }
});

var checkInput = function () {
    $('#search-input').on("input", function () {
        if ($(this).val() !== '') {
            $('#cancel').addClass('show-cancel');
        } else {
            $('#cancel').removeClass('show-cancel');
        }
    });
    $('#cancel').on('click', function () {
        $(this).removeClass('show-cancel');
        $('#search-input').val('');
    });
};

var checkInputHelp = function () {
    $('.blur-input').on("input", function () {
        if ($(this).val() !== '') {
            $('.cancel-default').addClass('show-cancel');
        } else {
            $('.cancel-default').removeClass('show-cancel');
        }
    });
    $('.cancel-default').on('click', function () {
        $(this).removeClass('show-cancel');
        $('.blur-input').val('');
    });
};

var loadBalanse = function () {
    $('.reload').on("click", function (e) {
        e.preventDefault();
        $(this).parent('.sum-option').addClass('is-active');
        setTimeout(function () { $('.sum-option').removeClass('is-active'); }, 200);
    });
};

var dropMenuUser = function () {
    $('#open--menu-user').on("click", function (e) {
        $(this).next('#drop--menu-user').toggleClass('is-active');
    });
    $(document).mouseup(function (e) {
        var container = $(".wrapper--user-login");
        if (container.has(e.target).length === 0) {
            $('#drop--menu-user').removeClass('is-active');
        }
    });
};

var filterOpen = function () {
    $('#filter-open').on('click', function () {
        $(this).next('.filter-hidden').toggleClass('is-active');
    });
    $(document).mouseup(function (e) {
        var container = $("#ui-datepicker-div");
        var container2 = $("#filter-button--wrapper");
        //if (container.has(e.target).length === 1) {

        //}
        // else
        if (container2.has(e.target).length === 0) {

            if ($(e.target).hasClass('ui-state-default') === false &&
                $(e.target).hasClass('ui-icon-circle-triangle-w') === false &&
                $(e.target).hasClass('ui-icon-circle-triangle-e') === false) { // prevent closing when picking date
                $('#filter-open').next('.filter-hidden').removeClass('is-active');
            }
        }
    });
};

//var selectNew = function () {
//    $(".select-phone").select2({
//        minimumResultsForSearch: -1,
//        id: "elementID",
//        text: "Hello!"
//    });
//    $(".select-phone").select2('data', { id: "elementID", text: "Hello!" });
//};

var countCode = function () {
    function startTimer(duration, display) {
        var timer = duration, minutes, seconds;
        setInterval(function () {
            minutes = parseInt(timer / 60, 10);
            seconds = parseInt(timer % 60, 10);
            minutes = minutes < 10 ? "0" + minutes : minutes;
            seconds = seconds < 10 ? "0" + seconds : seconds;
            display.textContent = minutes + ":" + seconds;

            if (--timer < 0) {
                timer = duration;
            }
        }, 1000);
    }

    var fiveMinutes = 60 * 1,
        display = document.querySelector('#time');


    $('.resend-code').on('click', function (e) {
        startTimer(fiveMinutes, display);
        e.preventDefault();
        $('.resend-time').removeClass('hidden');
    });

};

//var mediaQuery = function () {
//    var dropContainer = $('.credit-card--drop');
//    var mq = matchMedia('(max-width: 768px)');
//    var handler = function (e) {

//        if (e.matches) {

//            $(".popup").on('click', function () {
//                $('body').addClass('no-scroll');
//            });

//            dropContainer.find('.head--credit-card').addClass('choose-open')
//            dropContainer.find('.head--credit-card').on('click', function () {
//                $('.add-new--payment').removeClass('is-active');
//            });
//            $('.choose-open').on('click', function () {
//                $("#choose-payment").addClass("is-show");
//                $('body').addClass('no-scroll');
//            });
//            $('.add-new--payment-mobile .custom-close').on('click', function () {
//                $("#choose-payment").removeClass("is-show");
//                $('body').removeClass('no-scroll');
//            });

//        }
//        else {

//            $('.choose-open').on('click', function () {
//                $("#choose-payment").removeClass("is-show");
//                $('body').removeClass('no-scroll');
//            });

//            dropContainer.find('.head--credit-card').removeClass('choose-open')
//            dropContainer.find('.head--credit-card').on('click', function () {
//                $('.add-new--payment').toggleClass('is-active');
//            });
//            $(document).mouseup(function (e) {
//                var container = $(".head--credit-card");
//                if (container.has(e.target).length === 0) {
//                    $('.add-new--payment').removeClass('is-active');
//                }
//            });
//        }
//    };
//    mq.addListener(handler);
//    handler(mq);
//};

/* Popup Window */

/* Popup Window End */

// Popup Mobile

var popupMobile = function () {
    var linkPopup;

    $('.open-popup').click(function (e) {
        e.preventDefault();
        linkPopup = $($(this).attr('href'));
        linkPopup.addClass('open_window');
        linkPopup.find('.overlay-popup').addClass('is-active');
        $('body').addClass('no-scroll');
    });
    $('.custom-close, .overlay-popup').click(function () {
        $('.custom-popup').removeClass('open_window');
        $('.overlay-popup').removeClass('is-active');
        $('body').removeClass('no-scroll');
    });
};

// Crop Text

var cropText = function () {
    //console.log('1321');
    var sizeText = 70,
        newsContent = $('.message-tiket p'),
        newsText = newsContent.text();

    if (newsText.length > sizeText) {
        newsContent.text(newsText.slice(0, sizeText) + ' ...');
    }
};

// Edit link Mobile&Desctop

var linksData = $('.link-data'),
    linksHref,
    linksPopup;

linksData.each(function () {

    linksHref = $(this).attr("href");
    linksPopup = $(this).attr('href');

    var slice = $(this);

    if (matchMedia) {
        var screen = window.matchMedia("(max-width: 480px)");
        screen.addListener(changes);
        changes(screen);
    }

    function changes(screen) {
        if (screen.matches) {
            slice.attr('href', linksPopup);
            slice.removeClass('popup');
            slice.off('click');
            //console.log('2545');
        } else {
            slice.attr('href', linksHref);
            slice.addClass('popup');
        }
    }
});

$('body').on('click', '.custom-close', function () {
    $.modal.close();
});

$('.link-data').click(function (e) {
    e.preventDefault();
});
$.modal.defaults.clickClose = false;
$.modal.defaults.closeExisting = true;
$.modal.defaults.showClose = false;

$('[type=tel]').keypress(function (event) {
    if (event.which !== 8 && isNaN(String.fromCharCode(event.which))) {
        event.preventDefault(); //stop character from entering input
    }
});
function getUrlParameter(name) {
    name = name.replace(/[\[]/, '\\[').replace(/[\]]/, '\\]');
    var regex = new RegExp('[\\?&]' + name + '=([^&#]*)');
    var results = regex.exec(location.search);
    return results === null ? '' : decodeURIComponent(results[1].replace(/\+/g, ' '));
};

checkInput();
checkInputHelp();
loadBalanse();
dropMenuUser();
filterOpen();
//selectNew();
filterDate();
popupMobile();
//mediaQuery();
// countCode();