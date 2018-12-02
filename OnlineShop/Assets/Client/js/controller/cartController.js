var cart = {
    init: function () {
        cart.registerEvent();
    },
    registerEvent: function () {
        //tiep tuc mua hang
        $('#btnContinue').off('click').on('click', function () {
            window.location.href = "/";
        });

        //Thanh toan
        $('#btnPayment').off('click').on('click', function () {
            window.location.href = "/thanh-toan";
        });

        // Cap nhat gio hang
        $('#btnUpdate').off('click').on('click', function () {
            var listProduct = $('.txtQuantity'); // lay ra tat ca san pham

            //Tao danh sach chua san pham
            var cartList = [];

            //Lap qua tung san pham
            $.each(listProduct, function (i, item) {
                cartList.push({
                    Quantity: $(item).val(),
                    Product: {
                        ID: $(item).data('id')
                    }
                });
            });

            //Goi Ajax
            $.ajax({
                url: '/Cart/Update',
                type: 'POST',
                dataType: 'json',
                data: { cartModel: JSON.stringify(cartList) },
                success: function (response) {
                    if (response.status) {
                        window.location.href = "/gio-hang";
                    } else {

                    }
                }
            });
        });//End cap nhat gio hang

        // Xoa gio hang
        $('#btnDeleteAll').off('click').on('click', function () {
            $.ajax({
                url: '/Cart/DeleteAll',
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/gio-hang";
                    }
                }
            })
        });
        // End Xoa gio hang


        // Xoa gio hang
        $('.btn-delete').off('click').on('click', function (e) {
            e.preventDefault();
            $.ajax({
                data: { id: $(this).data('id') },
                url: '/Cart/Delete',
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/gio-hang";
                    }
                }
            })
        });
        // End Xoa gio hang
    }
}

cart.init();


