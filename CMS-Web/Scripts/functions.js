(function($)
{
	"use strict"

	/* Event - Window Scroll */
	$(window).scroll(function()
	{
		var scroll	=	$(window).scrollTop();
		var height	=	$(window).height();

		/*** set sticky menu ***/
		if( scroll >= 300 )
		{
			$('.menu-block').addClass("navbar-fixed-top").delay( 2000 ).fadeIn();
		}
		else if ( scroll <= height )
		{
			$('.menu-block').removeClass("navbar-fixed-top");
		}
		else
		{
			$('.menu-block').removeClass("navbar-fixed-top");
		} // set sticky menu - end		

		if ($(this).scrollTop() >= 50)
		{
			// If page is scrolled more than 50px
			$('#back-to-top').fadeIn(200);    // Fade in the arrow
		}
		else
		{
			$('#back-to-top').fadeOut(200);   // Else fade out the arrow
		}
	});
	/* Event - Window Scroll /- */

	$('#back-to-top').click(function()
	{
		// When arrow is clicked
		$('body,html').animate(
		{
			scrollTop : 0 // Scroll to top of body
		},800);
	});		
	
	$('.dial').each(function ()
	{
		var $this = $(this);
		var myVal = $(this).data("value");		

		$this.appear(function()
		{
			// alert(myVal);
			$this.knob({ });
			$({ value: 0 }).animate({ value: myVal },
			{
				duration: 2000,
				easing: 'swing',
				step: function ()
				{
					$this.val(Math.ceil(this.value)).trigger('change');
				}
			});
		});
	});
	
	
	/* Event - Document Ready /- */	
	$(document).ready(function($)
	{
		var scroll	=	$(window).scrollTop();
		var height	=	$(window).height();

		/*** set sticky menu ***/
		if( scroll >= height -500 )
		{
			$('.menu-block').addClass("navbar-fixed-top").delay( 2000 ).fadeIn();
		}
		else if ( scroll <= height )
		{
			$('.menu-block').removeClass("navbar-fixed-top");
		}
		else
		{
			$('.menu-block').removeClass("navbar-fixed-top");
		} // set sticky menu - end

		$('.navbar-nav li a, .logo-block a').on('click', function(event)
		{
			var anchor = $(this);

			if( anchor == 'undefined' || anchor == null || anchor.attr('href') == '#' ) { return; }
			if ( anchor.attr('href').indexOf('#') === 0 )
			{
				if( $(anchor.attr('href')).length )
				{
					$('html, body').stop().animate( { scrollTop: $(anchor.attr('href')).offset().top - 72 }, 1500, 'easeInOutExpo' );					
				}
				event.preventDefault();
			}
		});

		$('.goto-next a').on('click', function(event)
		{
			var anchor = $(this);

			if( anchor == 'undefined' || anchor == null || anchor.attr('href') == '#' ) { return; }
			if ( anchor.attr('href').indexOf('#') === 0 )
			{
				if( $(anchor.attr('href')).length )
				{
					$('html, body').stop().animate( { scrollTop: $(anchor.attr('href')).offset().top - 150 }, 1500, 'easeInOutExpo' );			
				}
				event.preventDefault();
			}
		});

		/* Window Hight Set to Elements /- */
		var window_height = $(window).height();
		var window_width = $(window).width();
		// $(".header").css("height", window_height + "px");
		
		/* Categories Section */
		$('.category-section').each(function ()
		{
			var $this = $(this);
			var myVal = $(this).data("value");

			$this.appear(function()
			{
				$('.category-section .category-box-main:nth-child(odd)').addClass('animated slideInLeft').delay( 10000 );			
				$('.category-section .category-box-main:nth-child(even)').addClass('animated slideInRight').delay( 10000 );			
			});
		});
		
		/* Featured Section */
		$('.featured-products').each(function ()
		{
			var $this = $(this);
			var myVal = $(this).data("value");

			$this.appear(function()
			{
				$('.featured-products .category-box-main:nth-child(odd)').addClass('animated slideInLeft').delay( 10000 );			
				$('.featured-products .category-box-main:nth-child(even)').addClass('animated slideInRight').delay( 10000 );			
			});
		});
		
		/* blog Section */
		$('.blog-section').each(function ()
		{
			var $this = $(this);
			var myVal = $(this).data("value");

			$this.appear(function()
			{
				$('.blog-section .col-md-3').addClass('animated slideInLeft').delay( 10000 );			
			});
		});
		
		/* About Section */
		$('.about-section').each(function ()
		{
			var $this = $(this);
			var myVal = $(this).data("value");

			$this.appear(function()
			{
				$('.about-section .about-content-box .col-md-6:nth-child(odd)').addClass('animated slideInLeft').delay( 10000 );			
				$('.about-section .about-content-box .col-md-6:nth-child(even)').addClass('animated slideInRight').delay( 10000 );			
			});
		});
		
		/* footer Section */
		$('.footer-section').each(function ()
		{
			var $this = $(this);
			var myVal = $(this).data("value");

			$this.appear(function()
			{
				$('.add-banner-section').addClass('animated fadeInUp').delay( 10000 );			
			});
		});
		
		/* contact-form Section */
		$('.contact-form-details').each(function ()
		{
			var $this = $(this);
			var myVal = $(this).data("value");

			$this.appear(function()
			{
				$('.contact-form').addClass('animated fadeInUp').delay( 10000 );			
				$('.contact-details').addClass('animated fadeInDown').delay( 10000 );			
			});
		});
		
		/* categories Section */
		$("#categories-list").owlCarousel(
		{
			autoPlay: 3000, //Set AutoPlay to 3 seconds

			items : 4,
			itemsDesktop : [1199,4],
			itemsDesktopSmall : [991,3],
			navigation : false,
			pagination: false
		});
		

		/* Partners Section */
		$("#our-partner").owlCarousel(
		{
			autoPlay: 3000, //Set AutoPlay to 3 seconds

			items : 4,
			itemsDesktop : [1199,4],
			itemsDesktopSmall : [991,3],
			navigation : false,
			pagination: false
		});
		
		/* Price Filter */
		$( "#slider-range" ).slider({
			range: true,
			min: 0,
			max: 15000,
			values: [ 0, 10000 ],
			slide: function( event, ui ) {
				$( "#amount" ).val( ui.values[ 0 ] + " $" ) 
				$( "#amount2" ).val(  ui.values[ 1 ] + " $" );
			}
		});
		
		$( "#amount" ).val( $( "#slider-range" ).slider( "values", 0 ) + " $" );
		$( "#amount2" ).val( $( "#slider-range" ).slider( "values", 1 ) + " $" );
				
		// Product Slider
		$('#about-carousel').flexslider({
			animation: "slide",
			controlNav: false,
			animationLoop: false,
			slideshow: false,
			itemWidth: 170,
			itemMargin: 5,
			asNavFor: '#about-slider',
			direction: "vertical"
		});

		$('#about-slider').flexslider({
			animation: "fade",
			controlNav: false,
			animationLoop: false,
			slideshow: false,
			sync: "#about-carousel",
			directionNav: false
		});

		/* Loader */
		$( '.loading a' ).on('click', function(e) {
			$( ".category-box-main.new-categories" ).removeClass('animated slideInLeft');
			$( ".category-box-main.new-categories" ).css( 'display','block' ).addClass("animated fadeInUp").delay( 2000 );
			$(".loading").css("display","none");
			return false;
		});
		
		/* Single product */
		$( '#color-widget li a' ).on( 'click', function(e) {
			$( "#color-widget li a" ).removeClass( 'active' );
			$(this).addClass( 'active' );
			return false;
		});
		
		$( '#size-widget li a' ).on( 'click', function(e) {
			$( "#size-widget li a" ).removeClass( 'active' );
			$(this).addClass( 'active' );
			return false;
		});
		
		/* Blog Listing */
		$( '.loading a' ).on( 'click', function(e) {
			$( ".type-post.new-post" ).css( 'display','inline-block' ).addClass("animated fadeInUp");
			$( ".loading" ).css( "display","none" );
			return false;
		});
		
		if( $(window).width() > 991 ) {
			$(".dropdown").hover(
				function() { $('.dropdown-menu', this).fadeIn("slow");
				},
				function() { $('.dropdown-menu', this).fadeOut("slow");
			});
		}
		/* Lightbox for Highlights Gallery	*/
		$('.product-box-inner').magnificPopup({
			delegate: ' ul > li:first-Child > a',
			type: 'image',
			tLoading: 'Loading image #%curr%...',
			mainClass: 'mfp-img-mobile',
			gallery: {
				enabled: true,
				navigateByImgClick: true,
				preload: [0,1] // Will preload 0 - before current, and 1 after the current image
			},
			image: {
				tError: '<a href="%url%">The image #%curr%</a> could not be loaded.',				
			}
		});
		
		$('.look-book-img-box-hover').magnificPopup({
			delegate: ' ul > li:first-Child > a',
			type: 'image',
			tLoading: 'Loading image #%curr%...',
			mainClass: 'mfp-img-mobile',
			gallery: {
				enabled: true,
				navigateByImgClick: true,
				preload: [0,1] // Will preload 0 - before current, and 1 after the current image
			},
			image: {
				tError: '<a href="%url%">The image #%curr%</a> could not be loaded.',				
			}
		});
		
		/* About Slider */
		$('#about-slider').magnificPopup({
			delegate: 'a',
			type: 'image',
			tLoading: 'Loading image #%curr%...',
			mainClass: 'mfp-img-mobile',
			gallery: {
				enabled: true,
				navigateByImgClick: true,
				preload: [0,1] // Will preload 0 - before current, and 1 after the current image
			},
			image: {
				tError: '<a href="%url%">The image #%curr%</a> could not be loaded.',				
			}
		});
		
		
		/* megnify */
		if( $(".magniflier").length ) {
		var native_width = 0;
		  var native_height = 0;
		  var mouse = {x: 0, y: 0};
		  var magnify;
		  var cur_img;

		  var ui = {
			magniflier: $('.magniflier')
		  };

		  // Add the magnifying glass
		  if (ui.magniflier.length) {
			var div = document.createElement('div');
			div.setAttribute('class', 'glass');
			ui.glass = $(div);

			$('body').append(div);
		  }

		  
		  // All the magnifying will happen on "mousemove"

		  var mouseMove = function(e) {
			var $el = $(this);

			// Container offset relative to document
			var magnify_offset = cur_img.offset();

			// Mouse position relative to container
			// pageX/pageY - container's offsetLeft/offetTop
			mouse.x = e.pageX - magnify_offset.left;
			mouse.y = e.pageY - magnify_offset.top;
			
			// The Magnifying glass should only show up when the mouse is inside
			// It is important to note that attaching mouseout and then hiding
			// the glass wont work cuz mouse will never be out due to the glass
			// being inside the parent and having a higher z-index (positioned above)
			if (
			  mouse.x < cur_img.width() &&
			  mouse.y < cur_img.height() &&
			  mouse.x > 0 &&
			  mouse.y > 0
			  ) {
				magnify(e);
			}
			else {
			  ui.glass.fadeOut(100);
			}
			return;
		  };

		  var magnify = function(e) {

			// The background position of div.glass will be
			// changed according to the position
			// of the mouse over the img.magniflier
			//
			// So we will get the ratio of the pixel
			// under the mouse with respect
			// to the image and use that to position the
			// large image inside the magnifying glass

			var rx = Math.round(mouse.x/cur_img.width()*native_width - ui.glass.width()/2)*-1;
			var ry = Math.round(mouse.y/cur_img.height()*native_height - ui.glass.height()/2)*-1;
			var bg_pos = rx + "px " + ry + "px";
			
			// Calculate pos for magnifying glass
			//
			// Easy Logic: Deduct half of width/height
			// from mouse pos.

			// var glass_left = mouse.x - ui.glass.width() / 2;
			// var glass_top  = mouse.y - ui.glass.height() / 2;
			var glass_left = e.pageX - ui.glass.width() / 2;
			var glass_top  = e.pageY - ui.glass.height() / 2;
			//console.log(glass_left, glass_top, bg_pos)
			// Now, if you hover on the image, you should
			// see the magnifying glass in action
			ui.glass.css({
			  left: glass_left,
			  top: glass_top,
			  backgroundPosition: bg_pos
			});

			return;
		  };

		  $('.magniflier').on('mousemove', function() {
			ui.glass.fadeIn(100);
			
			cur_img = $(this);

			var large_img_loaded = cur_img.data('large-img-loaded');
			var src = cur_img.data('large') || cur_img.attr('src');

			// Set large-img-loaded to true
			// cur_img.data('large-img-loaded', true)

			if (src) {
			  ui.glass.css({
				'background-image': 'url(' + src + ')',
				'background-repeat': 'no-repeat'
			  });
			}

			// When the user hovers on the image, the script will first calculate
			// the native dimensions if they don't exist. Only after the native dimensions
			// are available, the script will show the zoomed version.
			//if(!native_width && !native_height) {

			  if (!cur_img.data('native_width')) {
				// This will create a new image object with the same image as that in .small
				// We cannot directly get the dimensions from .small because of the 
				// width specified to 200px in the html. To get the actual dimensions we have
				// created this image object.
				var image_object = new Image();

				image_object.onload = function() {
				  // This code is wrapped in the .load function which is important.
				  // width and height of the object would return 0 if accessed before 
				  // the image gets loaded.
				  native_width = image_object.width;
				  native_height = image_object.height;

				  cur_img.data('native_width', native_width);
				  cur_img.data('native_height', native_height);

				  //console.log(native_width, native_height);

				  mouseMove.apply(this, arguments);

				  ui.glass.on('mousemove', mouseMove);
				};

				image_object.src = src;
				
				return;
			  } else {

				native_width = cur_img.data('native_width');
				native_height = cur_img.data('native_height');
			  }
			//}
			//console.log(native_width, native_height);

			mouseMove.apply(this, arguments);

			ui.glass.on('mousemove', mouseMove);
		  });

		  ui.glass.on('mouseout', function() {
			ui.glass.off('mousemove', mouseMove);
		  });
		}
		
		$("#default").on("click",function()
		{
			$("#color").attr("href", "css/color-schemes/default.css");
			return false;
		});
		
		$("#cyan").on("click",function()
		{
			$("#color").attr("href", "css/color-schemes/cyan.css");
			return false;
		});
		
		$("#dark-blue").on("click",function()
		{
			$("#color").attr("href", "css/color-schemes/dark-blue.css");
			return false;
		});
		
		$("#green").on("click",function()
		{
			$("#color").attr("href", "css/color-schemes/green.css");
			return false;
		});
		
		$("#red").on("click",function()
		{
			$("#color").attr("href", "css/color-schemes/red.css");
			return false;
		});
		
		$("#yellow").on("click",function()
		{
			$("#color").attr("href", "css/color-schemes/yellow.css");
			return false;
		});

		$("#light-green").on("click",function()
		{
			$("#color").attr("href", "css/color-schemes/light-green.css");
			$('.dial').trigger('configure',{"fgColor":"#64E294"});
			return false;
		});

		$("#orange").on("click",function()
		{
			$("#color").attr("href", "css/color-schemes/orange.css");
			return false;
		});

		$("#pink").on("click",function()
		{
			$("#color").attr("href", "css/color-schemes/pink.css");
			return false;
		});

		$("#black").on("click",function()
		{
			$("#color").attr("href", "css/color-schemes/black.css");
			return false;
		});

		// picker buttton
		$(".picker_close").on("click",function()
		{
			$("#choose_color").toggleClass("position");
		});
		
		/* gMAP */
		$("#gmap").gMap({
			controls: false,
			scrollwheel: true,
			
			markers: [
				{
					latitude: 47.670553,
					longitude: 9.588479,
					icon: {
						image: "images/map-marker.png",
						iconsize: [26, 46],
						iconanchor: [12,46]
					}
				},
				{
					latitude: 47.65197522925437,
					longitude: 9.47845458984375
				},
				{
					latitude: 47.594996,
					longitude: 9.600708,
					icon: {
						image: "images/map-marker.png",
						iconsize: [26, 46],
						iconanchor: [12,46]
					}
				}
			],
			icon: {
				image: "images/map-marker.png", 
				iconsize: [26, 46],
				iconanchor: [12, 46]
			},
			latitude: 47.58969,
			longitude: 9.473413,
			zoom: 10
		});
	
	});	
	/* document.ready /- */		
	
	/* Event - Window Resize /- */
	$(window).resize(function()
	{
		/* Window Hight Set to Elements /- */
		var window_height = $(window).height();
		var window_width = $(window).width();
		// $(".header").css("height", window_height + "px");
		
	});
	/* Event - Window Resize /- */

	var $itemsHolder = $('.portfolio-list');
	var $itemsClone = $itemsHolder.clone(); 
	var $filterClass = "";
	$('.portfolio-categories li').click(function(e)
	{
		e.preventDefault();
		$filterClass = $(this).attr('data-value');
		if($filterClass == 'all')
		{
			var $filters = $itemsClone.find('li');
		}
		else
		{
			var $filters = $itemsClone.find('li[data-type='+ $filterClass +']');
		}
		$itemsHolder.quicksand(
		$filters,
		{
			duration: 1000
		});
	});
	
	$('.portfolio-categories li a').on('click', function(e) {
		$(".portfolio-categories li a").removeClass('active');
		$(this).addClass('active');
	});
	
	var wow = new WOW(
	{
		boxClass:     'wow',      // animated element css class (default is wow)
		animateClass: 'animated', // animation css class (default is animated)
		offset:       0,          // distance to the element when triggering the animation (default is 0)
		mobile:       true,       // trigger animations on mobile devices (default is true)
		live:         true        // act on asynchronously loaded content (default is true)
	});
	wow.init();
	
	
	if (!$('html').is('.ie6, .ie7, .ie8'))
	{
		/* Event - Window Load */
		$(window).load(function()
		{		
			/* Loader */
			$("#site-loader").delay(1000).fadeOut("slow");
		});
		/* Event - Window Load /- */
	}
	else
	{
		$("#site-loader").css('display','none');
	}
	
})(jQuery);

jQuery(document).ready(function(){
	
	// This button will increment the value
	$('.qtyplus').on('click', function(e){
		// Stop acting like a button
		e.preventDefault();

		// Get the field name
		fieldName = $(this).attr('data-field');

		// Get its current value
		var currentVal = parseInt($('input[name='+fieldName+']').val());
		
		// If is not undefined
		if (!isNaN(currentVal)) {
			// Increment
			$('input[name='+fieldName+']').val(currentVal + 1);
		} else {
			// Otherwise put a 0 there
			$(this).find('input[name='+fieldName+']').val(0);
		}
	});

	// This button will decrement the value till 0
	$(".qtyminus").on( 'click', function(e) {
		// Stop acting like a button
		e.preventDefault();
		// Get the field name
		fieldName = $(this).attr('data-field');
		// Get its current value
		var currentVal = parseInt($('input[name='+fieldName+']').val());
		// If it isn't undefined or its greater than 0
		if (!isNaN(currentVal) && currentVal > 0) {
			// Decrement one
			$('input[name='+fieldName+']').val(currentVal - 1);
		} else {
			// Otherwise put a 0 there
			$('input[name='+fieldName+']').val(0);
		}
	});
});
