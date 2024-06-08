$(document).ready(function () {
    var stars = $('.star');

    // Hàm xử lý khi bạn hover vào một sao
    stars.on('mouseenter', function () {
        var index = stars.index(this); // Lấy vị trí của sao bạn đang hover
        stars.removeClass('selected'); // Xóa class 'selected' của tất cả các sao
        stars.slice(0, index + 1).addClass('selected'); // Thêm class 'selected' cho các sao từ đầu đến sao bạn đang hover
    });

    // Hàm xử lý khi bạn rời chuột khỏi các sao
    $('.stars').on('mouseleave', function () {
        var rating = $('#rating').val(); // Lấy giá trị rating từ hidden field
        stars.removeClass('selected'); // Xóa class 'selected' của tất cả các sao
        stars.slice(0, rating).addClass('selected'); // Thêm class 'selected' cho các sao từ đầu đến rating
    });

    // Hàm xử lý khi bạn bấm vào một sao
    stars.on('click', function () {
        var index = stars.index(this); // Lấy vị trí của sao bạn đang click
        $('#rating').val(index + 1); // Cập nhật giá trị rating vào hidden field
        stars.removeClass('selected'); // Xóa class 'selected' của tất cả các sao
        stars.slice(0, index + 1).addClass('selected'); // Thêm class 'selected' cho các sao từ đầu đến sao bạn đang click
    });
});
