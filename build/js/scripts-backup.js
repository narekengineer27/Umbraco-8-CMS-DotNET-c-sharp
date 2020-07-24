//= jquery.min.js
//= jquery.magnific-popup.min.js
//= select.js
//= select2.full.js
//= jquery-ui.js
//= jquery.payment.min.js

// window.onload = function(){
//   // $("body").toggleClass('opacity');
//   $("#overlayer").delay(1000).fadeOut("slow");
//   overlay.toggle();
//   setTimeout(function() {
//     overlay.toggle();
//   }, 800);
// }

var filterDate = function() {
	var minDate = new Date();
	$( ".datapicker" ).datepicker({
		dateFormat: 'dd.mm.yy',
		minDate: minDate,
	}).val();
	$(".datapicker").attr('autocomplete', 'off');
	$('.cc-number').formatCardNumber();
};


function heightFooter(){
	var heightFoot = $('.footer').outerHeight();
	$('body').css({ 'padding-bottom': heightFoot});
}
heightFooter()
$( window ).resize(function() {
	heightFooter()
});

$(".burger-menu").on("click", function(e) {
	e.preventDefault(); 
	$(this).toggleClass('is-active');
	$('.top-line').toggleClass('is-active');
	$('.mobile-menu').toggleClass('is-active');
	$("body").toggleClass('overflow');
});
$(document).mouseup(function (e) {
	var container = $(".mobile-menu, .burger-menu");
	if (container.has(e.target).length === 0){
		$('.burger-menu').removeClass('is-active');
		$('.top-line').removeClass('is-active');
		$('.mobile-menu').removeClass('is-active');
		$("body").removeClass('overflow');
	}
});

$('.search-link').on("click", function(e) {
	e.preventDefault(); 
  $(this).toggleClass('is-active');
	$(this).parents('.head-search__search').toggleClass('no-border');
	$('.search-form--head').toggleClass('is-active');
});
$(document).mouseup(function (e) {
  var container = $(".head-search");
  if (container.has(e.target).length === 0){
  	$('.search-link').removeClass('is-active');
		$('.search-link').parents('.head-search__search').removeClass('no-border');
    $('.search-form--head').removeClass('is-active');
  }
});

var checkInput = function(){
	$('#search-input').on("input", function() {
		if ($(this).val()!=''){
			$('#cancel').addClass('show-cancel');
		} else {
			$('#cancel').removeClass('show-cancel');
		}
	});
	$('#cancel').on('click', function() {
		$(this).removeClass('show-cancel');
		$('#search-input').val('');
	});
};

var checkInputHelp = function(){
	$('.blur-input').on("input", function() {
		if ($(this).val()!=''){
			$('.cancel-default').addClass('show-cancel');
		} else {
			$('.cancel-default').removeClass('show-cancel');
		}
	});
	$('.cancel-default').on('click', function() {
		$(this).removeClass('show-cancel');
		$('.blur-input').val('');
	});
};

var loadBalanse = function(){
	$('.reload').on("click", function(e) {
		e.preventDefault();
		$(this).parent('.sum-option').addClass('is-active');
		setTimeout( function() {$('.sum-option').removeClass('is-active');}, 200);
	});
};

var dropMenuUser = function() {
	$('#open--menu-user').on("click", function(e) {
		$(this).next('#drop--menu-user').toggleClass('is-active');
	});
	$(document).mouseup(function (e) {
		var container = $(".wrapper--user-login");
		if (container.has(e.target).length === 0){
			$('#drop--menu-user').removeClass('is-active');
		}
	});
};

var filterOpen = function() {
	$('#filter-open').on('click', function() {
		$(this).next('.filter-hidden').toggleClass('is-active');
	});
	$(document).mouseup(function (e) {
		var container = $("#ui-datepicker-div");
		var container2 = $("#filter-button--wrapper");
		if (container.has(e.target).length === 1){
			
		}
		else if( container2.has(e.target).length === 0) {
			$('#filter-open').next('.filter-hidden').removeClass('is-active');
		}
	});
};

var selectNew = function() {
	$(".select-phone").select2({
    minimumResultsForSearch: -1,
    id:"elementID",
    text: "Hello!"
  });
  $(".select-phone").select2('data', { id:"elementID", text: "Hello!"});
};

var cardDrop = function() {
	// var dropContainer = $('.credit-card--drop');
	// dropContainer.find('.head--credit-card').on('click', function() {
	// 	$('.add-new--payment').addClass('is-active');
	// 	// $(this).next('.main--credit-card').slideToggle(400);
	// 	// $(this).toggleClass('is-active');
	// 	// $(this).next('.main--credit-card').find('.wrapper-show--animate').toggleClass('is-show');
	// });
	// $(document).mouseup(function (e) {
	// 	var container = $(".head--credit-card");
	// 	if (container.has(e.target).length === 0){
	// 		$('.add-new--payment').removeClass('is-active');
	// 	}
	// });
};

var countCode = function() {
	function startTimer(duration, display) {
		var timer = duration, minutes, seconds;
		setInterval(function () {
			minutes = parseInt(timer / 60, 10)
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
	

	$('.resend-code').on('click', function(e){
		startTimer(fiveMinutes, display);
		e.preventDefault();
		$('.resend-time').removeClass('hidden');
	});
	
};
// $('.resend-code').on('click', function(){
// 	countCode()
// 	// e.preventDefault();
// 	$('.resend-time').removeClass('hidden');
// });


var mediaQuery = function() {
	var dropContainer = $('.credit-card--drop');
	var mq = matchMedia('(max-width: 768px)');
	var handler = function(e) {

		if (e.matches) {

			$(".popup").on('click', function(){
				$('body').addClass('no-scroll');
			});

			dropContainer.find('.head--credit-card').addClass('choose-open')
			dropContainer.find('.head--credit-card').on('click', function() {
				$('.add-new--payment').removeClass('is-active');
			});
			$('.choose-open').on('click', function() {
				$("#choose-payment").addClass("is-show");
				$('body').addClass('no-scroll');
			});
			$('.add-new--payment-mobile .custom-close').on('click', function() {
				$("#choose-payment").removeClass("is-show");
				$('body').removeClass('no-scroll');
			});

		}
		else {

			$('.choose-open').on('click', function() {
				$("#choose-payment").removeClass("is-show");
				$('body').removeClass('no-scroll');
			});

			dropContainer.find('.head--credit-card').removeClass('choose-open')
			dropContainer.find('.head--credit-card').on('click', function() {
				$('.add-new--payment').toggleClass('is-active');
			});
			$(document).mouseup(function (e) {
				var container = $(".head--credit-card");
				if (container.has(e.target).length === 0){
					$('.add-new--payment').removeClass('is-active');
				}
			});


			// document.body.classList.remove('mobile-body');
		}
	};
	mq.addListener(handler);
	handler(mq);
};


/* Popup Window */

var initPopup = function () {
	$(".popup").magnificPopup({
		type: 'inline',
		removalDelay: 300,
		mainClass: 'my-mfp-slide-bottom',
		closeMarkup: '<div class="custom-close">x</div>',
		callbacks: {
			open: function(){
				$.magnificPopup.instance.close = function () {
					$('body').removeClass('no-scroll');
					$.magnificPopup.proto.close.call(this);
				};
			}
		}
	});
	$(document).on('click', '.custom-close', function (e) {
		e.preventDefault();
		$.magnificPopup.close();
	});

};

$('#popup-close').on('click', function(e) {
	e.preventDefault();
	$.magnificPopup.close();
});



/* Popup Window End */

// Popup Mobile

var popupMobile = function() {
	var linkPopup;

	$('.open-popup').click(function(e) {
		e.preventDefault();
		linkPopup = $($(this).attr('href'));
		linkPopup.addClass('open_window');
		linkPopup.find('.overlay-popup').addClass('is-active');
		$('body').addClass('no-scroll');
	});
	$('.custom-close, .overlay-popup').click(function() {
		$('.custom-popup').removeClass('open_window');
		$('.overlay-popup').removeClass('is-active');
		$('body').removeClass('no-scroll');
	});
};

// Crop Text

var cropText = function() {
	console.log('1321')
	var sizeText = 70,
	newsContent= $('.message-tiket p'),
	newsText = newsContent.text();

	if(newsText.length > sizeText){
		newsContent.text(newsText.slice(0, sizeText) + ' ...');
	}
};

// Edit link Mobile&Desctop

var linksData = $('.link-data'),
		linksHref,
		linksPopup;

linksData.each(function() {

	linksHref =  $(this).attr("href");
	linksPopup = $(this).attr('data-mobilelink');

	var slice = $(this);

	if(matchMedia){
	var screen = window.matchMedia("(max-width: 480px)");
	screen.addListener(changes);
	changes(screen);
}

	function changes(screen){
	if(screen.matches){
		slice.attr('href', linksPopup);
		slice.removeClass('popup');
		slice.off('click');
console.log('2545')
		// cropText();
	}else{
		slice.attr('href',linksHref);
		slice.addClass('popup');
		initPopup();

	}
}
});



// var mobileDesctop = function(e) {

// 	linksData.each(function() {

// 		linksHref =  $(this).attr("href");
// 		linksPopup = $(this).attr('data-mobilelink');

// 		temp1 = linksHref;
// 		temp2 = linksPopup;

// 			if (e.matches) {
// 				$(this).attr('href',linksPopup);
// 				$(this).attr('data-mobilelink', linksHref);

// 				// linksHref = linksHref;
// 				// linksPopup = linksPopup;

// 				$(this).off('click');
// 				$(this).removeClass('popup');
// 			}
// 			else {
// 				$(this).attr('href',temp1);
// 				$(this).attr('data-mobilelink', temp2);

// 				$(this).addClass('popup');
// 			}

// 		// console.log(linksPopup)
// 		// console.log(linksHref)
// 	});

// };
// sq.addListener(mobileDesctop);
// mobileDesctop(sq);


checkInput();
checkInputHelp();
loadBalanse();
dropMenuUser();
filterOpen();
selectNew();
filterDate();
cardDrop();
popupMobile();
mediaQuery();
initPopup();

// countCode();