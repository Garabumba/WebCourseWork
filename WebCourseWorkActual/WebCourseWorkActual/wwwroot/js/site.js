// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function ChangeBalance() {
    // Получаем JSON-данные из LocalStorage
    //var tableDataJson = localStorage.getItem('tableData');
    var newBalance = parseFloat($('.balance').val());

    // Отправляем данные на сервер
    $.ajax({
        url: '/Check/ChangeBalance',
        type: 'POST',
        data: { newValue: newBalance },
        success: function (data) {
            console.log('Данные успешно отправлены на сервер');
        },
        error: function (xhr, status, error) {
            console.error('Ошибка при отправке данных на сервер:', error);
        }
    });
}

$(document).ready(function () {
    $('.deleteExpenseButton').click(function () {
        var expenseName = $(this).parent().parent().contents().filter(function () {
            return this.nodeType === 3; // Фильтруем только текстовые узлы
        }).text().trim();
        $(this).parent().parent().remove();
        //console.log(expense);
        $.ajax({
            url: '/Categories/DeleteExpenseCategory',
            type: 'POST',
            data: { name: expenseName },
            success: function (data) {
                console.log('Данные успешно отправлены на сервер');
            },
            error: function (xhr, status, error) {
                console.error('Ошибка при отправке данных на сервер:', error);
            }
        });
    });

    $('.deleteIncomeButton').click(function () {
        var incomeName = $(this).parent().parent().contents().filter(function () {
            return this.nodeType === 3; // Фильтруем только текстовые узлы
        }).text().trim();
        $(this).parent().parent().remove();
        //console.log(expense);
        $.ajax({
            url: '/Categories/DeleteIncomeCategory',
            type: 'POST',
            data: { name: incomeName },
            success: function (data) {
                console.log('Данные успешно отправлены на сервер');
            },
            error: function (xhr, status, error) {
                console.error('Ошибка при отправке данных на сервер:', error);
            }
        });
    });

    $('.changeExpenseButton').click(function () {
        var expenseName = $(this).parent().parent().contents().filter(function () {
            return this.nodeType === 3; // Фильтруем только текстовые узлы
        }).text().trim();

        var url = '/Categories/ChangeExpenseCategory?oldName=' + encodeURIComponent(expenseName);
        window.location.href = url;
    });

    $('.changeIncomeButton').click(function () {
        var incomeName = $(this).parent().parent().contents().filter(function () {
            return this.nodeType === 3; // Фильтруем только текстовые узлы
        }).text().trim();

        var url = '/Categories/ChangeIncomeCategory?oldName=' + encodeURIComponent(incomeName);
        window.location.href = url;
    });
});