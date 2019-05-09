let addToTheCartItems = document.querySelectorAll('.store-item__add');
let removeFromTheCartItems = document.querySelectorAll('.store-item__remove');
const cartItemsCount = document.querySelector('.сart__count');

window.onload = function () {
    $.get('../cart/count/', (data) => {
        cartItemsCount.innerHTML = '';
        cartItemsCount.innerHTML = data;
    });
}
setHandlers(addToTheCartItems, './cart/add/');
setHandlers(removeFromTheCartItems, './cart/remove/');



function setHandlers(selector, url) {

    selector.forEach(item => {
        item.addEventListener('click', () => {
            let b = item.parentNode.firstChild.nextSibling.textContent.trim();
            $.ajax({
                url: url,
                type: 'Post',
                data: JSON.stringify(b),
                contentType: 'application/json',
                dataType: 'json',
                success: function (data) {

                    cartItemsCount.innerHTML = '';
                    if (data != '0') {
                        cartItemsCount.innerHTML = data;
                    }
                }
            });
        })
    });
}