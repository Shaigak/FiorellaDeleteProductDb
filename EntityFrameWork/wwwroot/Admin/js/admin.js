$(function () {
    
    let productId = $(this).parent().attr("data-id");
    console.log(productId)
    let data = {id:productId}


    $(document).on("click", ".slider-status", function () {
        
        let productId = $(this).parent().attr("data-id");
        let data = { id: productId }
        let changeElem = $(this);
        $.ajax({
           
            url: "slider/setstatus",
            type: "Post",
            data:data,
            success: function (res) {

              
                if (res) {
                    $(changeElem).removeClass("active-status")
                    $(changeElem).addClass("deActive-status")
                } else {
                    $(changeElem).addClass("active-status")
                    $(changeElem).removeClass("deActive-status")
                }

            }

        })
    })


    $(document).on("submit", "#category-delete-form", function () {

        let categoryId = $(this).attr("data-id");
        let data = { id: categoryId }
        let deletedElem = $(this);



        $.ajax({
            url: `category/softdelete`,
            type: "POST",
            data:data,
            success: function (res) {
                $(deletedElem).parent().parent().remove();
            }
        })
        
    })





})