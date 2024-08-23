document.querySelectorAll('.border').forEach(function (card) {
    card.addEventListener('click', function () {
        // 找到这个卡片下的详细信息部分
        var details = card.querySelector('.card-details');

        // 切换显示和隐藏
        if (details.classList.contains('show')) {
            details.classList.remove('show');
            details.classList.add('hide');
        } else {
            details.classList.remove('hide');
            details.classList.add('show');
        }
    });
});