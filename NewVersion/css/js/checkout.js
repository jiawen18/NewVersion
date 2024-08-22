document.addEventListener("DOMContentLoaded", function () {
    const paymentMethods = document.querySelectorAll('.transfer');

    paymentMethods.forEach(function (method) {
        method.addEventListener('click', function () {
            // 移除其他元素的 active 类
            paymentMethods.forEach(function (item) {
                item.classList.remove('active');
            });

            // 为当前点击的元素添加 active 类
            this.classList.add('active');
        });
    });
});
