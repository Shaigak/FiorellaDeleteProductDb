

$(document).ready(function () {




    $(document).on("click", ".load-more", function () {

        let parent = $(".parent-products");

        let skipCount =$(parent).children().length

        

        $.ajax({
            url: `product/loadmore?skip=${skipCount}`,
            type: "Get",
            success: function (res) {
                $(parent).append(res)

                if (res.trim() === "") {
                    $(".load-more").addClass("d-none")
                }


            }
        })

    })


    $(document).on("submit", ".price form", function () {

        let id = $(this).attr("data-id");

        

        $.ajax({
            url: `home/addbasket?id=${id}`,
            type: "POST",
            success: function (res) {
                swal("Product added to basket", "", "success");

             $(".cart-count").text(res)
    
            }
        }) 
        return false;
    })


    $(document).on("click", ".deleteProd", function () {


        let id = $(this).parent().parent().attr("data-id");

        let product = $(this).parent().parent();

        


        $.ajax({
            type: "Get",
            url: `Cart/Delete?id=${id}`,
            success: function (res) {

                $(product).remove(); 
                swal("Product deleted to basket", "", "success");


            }

        })


    })

    $(document).on("click", ".minus", function () {
        let id = $(this).parent().parent().parent().attr("data-id")
        let count = $(this).parent().next();

       

        $.ajax({
            type: "Get",
            url: `cart/MinusProductFromBasket?id=${id}`,
            success: function (res) {



                res--;
                $(count).text(res);
                /*location.reload();*/
            }
        })
    })


    $(document).on("click", ".plus", function () {
        let id = $(this).parent().parent().parent().attr("data-id")
        let count = $(this).parent().prev();
        let total = $(this).parent().parent().next().next()

        console.log(total)

        
 
        
        $.ajax({
            type: "Get",
            url: `cart/PlusProductFromBasket?id=${id}`,
            success: function (res) {
                

                res++;
                $(count).text(res);
                $(total).text * (res);



            }
        })


    })


   

    $(document).on('click', '#search', function () {
        $(this).next().toggle();
    })

    $(document).on('click', '#mobile-navbar-close', function () {
        $(this).parent().removeClass("active");

    })
    $(document).on('click', '#mobile-navbar-show', function () {
        $('.mobile-navbar').addClass("active");

    })

    $(document).on('click', '.mobile-navbar ul li a', function () {
        if ($(this).children('i').hasClass('fa-caret-right')) {
            $(this).children('i').removeClass('fa-caret-right').addClass('fa-sort-down')
        }
        else {
            $(this).children('i').removeClass('fa-sort-down').addClass('fa-caret-right')
        }
        $(this).parent().next().slideToggle();
    })

    // SLIDER

    $(document).ready(function(){
        $(".slider").owlCarousel(
            {
                items: 1,
                loop: true,
                autoplay: true
            }
        );
      });

    // PRODUCT

    $(document).on('click', '.categories', function(e)
    {
        e.preventDefault();
        $(this).next().next().slideToggle();
    })

    $(document).on('click', '.category li a', function (e) {
        e.preventDefault();
        let category = $(this).attr('data-id');
        let products = $('.product-item');
        
        products.each(function () {
            if(category == $(this).attr('data-id'))
            {
                $(this).parent().fadeIn();
            }
            else
            {
                $(this).parent().hide();
            }
        })
        if(category == 'all')
        {
            products.parent().fadeIn();
        }
    })

    // ACCORDION 

    $(document).on('click', '.question', function()
    {   
       $(this).siblings('.question').children('i').removeClass('fa-minus').addClass('fa-plus');
       $(this).siblings('.answer').not($(this).next()).slideUp();
       $(this).children('i').toggleClass('fa-plus').toggleClass('fa-minus');
       $(this).next().slideToggle();
       $(this).siblings('.active').removeClass('active');
       $(this).toggleClass('active');
    })

    // TAB

    $(document).on('click', 'ul li', function()
    {   
        $(this).siblings('.active').removeClass('active');
        $(this).addClass('active');
        let dataId = $(this).attr('data-id');
        $(this).parent().next().children('p.active').removeClass('active');

        $(this).parent().next().children('p').each(function()
        {
            if(dataId == $(this).attr('data-id'))
            {
                $(this).addClass('active')
            }
        })
    })

    $(document).on('click', '.tab4 ul li', function()
    {   
        $(this).siblings('.active').removeClass('active');
        $(this).addClass('active');
        let dataId = $(this).attr('data-id');
        $(this).parent().parent().next().children().children('p.active').removeClass('active');

        $(this).parent().parent().next().children().children('p').each(function()
        {
            if(dataId == $(this).attr('data-id'))
            {
                $(this).addClass('active')
            }
        })
    })

    // INSTAGRAM

    $(document).ready(function(){
        $(".instagram").owlCarousel(
            {
                items: 4,
                loop: true,
                autoplay: true,
                responsive:{
                    0:{
                        items:1
                    },
                    576:{
                        items:2
                    },
                    768:{
                        items:3
                    },
                    992:{
                        items:4
                    }
                }
            }
        );
      });

      $(document).ready(function(){
        $(".say").owlCarousel(
            {
                items: 1,
                loop: true,
                autoplay: true
            }
        );
      });
})