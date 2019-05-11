let addToTheCartItems = document.querySelectorAll('.cart-add');
let removeFromTheCartItems = document.querySelectorAll('.сart-remove');
const cartItemsCount = document.querySelector('.сart__count');
const cartEmptyMessage = document.querySelector('.cart-empty');
const jsonTableButton = document.querySelector('.get-json__button');
const totalProductsSum = document.querySelector('.product-sum__Total');

window.onload = function () {

    if ((cartEmptyMessage != null || cartEmptyMessage != undefined)) {
        if (document.querySelector('.table-products').getElementsByTagName("tr").length > 0) {
            cartEmptyMessage.style.display = 'none';
        }
    }

    if (jsonTableButton != null || jsonTableButton != undefined) {
        jsonTableButton.addEventListener('click', getJsonData);
    }

    setHandlers(addToTheCartItems, '../cart/add/');
    setHandlers(removeFromTheCartItems, '../cart/remove/');

    $.get('../cart/count/', (data) => {
        cartItemsCount.innerHTML = '';
        cartItemsCount.innerHTML = data;
    });

}

function setHandlers(selector, url) {

    selector.forEach(item => {
        item.addEventListener('click', () => {
            let b = item.parentElement.querySelector(".store-item__Cart-id").textContent;
            $.ajax({
                url: url,
                type: 'Post',
                data: JSON.stringify(b),
                contentType: 'application/json',
                dataType: 'json',
                success: function (data) {
                    cartItemsCount.innerHTML = '';
                    cartItemsCount.innerHTML = data;
                    let method = url.includes('add') ? 'add' : 'remove';
                    ifSucces(item, method);
                }
            });
        })
    });
}

function ifSucces(clickedItem, method) {

    let info = clickedItem.parentElement.querySelector('.store-item__count');
    if ((info != null || info != undefined) && +info.textContent != 0) {
        let value = 0;
        if (method.includes('add')) {
            value = +info.textContent + 1;
        } else {
            value = +info.textContent - 1;
        }
        info.innerHTML = "";
        info.innerHTML = value;
        if (value == 0) {
            clickedItem.parentElement.parentElement.remove();
        }
        let count = document.querySelector('.table-products').getElementsByTagName('tr').length;

        if (count == 0 || count == undefined) {
            cartEmptyMessage.style.display = 'block';
        }

        $.get('../cart/sum', (data) => {
            if (!(isNaN(data) || data < 0)) {
                totalProductsSum.innerHTML = (+data).toFixed(2) + ' $';
            }
        });
    }
}

function getJsonData() {
    $.get('../cart/about/', (data) => {
        let s = document.querySelector('.helper__table');
        displayData(s, data)
    });
}

function displayData(whereInsertElement, data) {

    whereInsertElement.innerHTML = "";
    let tbl = document.createElement("table");
    let tblBody = document.createElement("tbody");
    let tblHead = document.createElement("thead");

    if (data !== null) {

        let head_row = document.createElement("tr");

        for (var prop_name in data[0]) {
            let th = document.createElement("th");
            th.innerHTML = prop_name;
            head_row.appendChild(th);
        }

        tblHead.appendChild(head_row);
        tbl.appendChild(tblHead);

        data.forEach(element => {
            let row = document.createElement("tr");

            Object.values(element).forEach(value => {
                let td = document.createElement("td");
                if (typeof (value) === 'object') {
                    let result = "";
                    for (prop in value) {
                        result += prop + ': ' + value[prop] + ';<br/>'
                    }
                    td.innerHTML = result;
                }
                else {
                    td.innerHTML = value;
                }
                row.appendChild(td);
            });
            tblBody.appendChild(row);
        });
    }

    tbl.appendChild(tblBody);
    whereInsertElement.appendChild(tbl);
    tbl.setAttribute("class", "table my-table");

}
