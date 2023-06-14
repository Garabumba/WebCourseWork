// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function ChangeBalance() {
    // Получаем JSON-данные из LocalStorage
    //var tableDataJson = localStorage.getItem('tableData');
    var newBalance = parseFloat($('.balance').val().replace(",", "."));

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

    /*===========================================================================================*/
    $('.deleteExpense').click(function (e) {
        expenseId = parseInt(e.target.getAttribute('expense-id'));
        expenseSum = parseFloat(e.target.getAttribute('expense-balance-value').replace(",", "."));
        actualBalance = $('.actualBalance').text().split(' ')[1].replace(",", ".");
        expenseSum += parseFloat(actualBalance);
        $('.actualBalance').text('Баланс: ' + expenseSum);

        $(this).parent().parent().remove();
        $.ajax({
            url: '/Home/DeleteExpense',
            type: 'POST',
            data: { expenseId: expenseId },
            success: function (data) {
                console.log('Данные успешно отправлены на сервер');
            },
            error: function (xhr, status, error) {
                console.error('Ошибка при отправке данных на сервер:', error);
            }
        });
    });

    $('.deleteIncome').click(function (e) {
        incomeId = parseInt(e.target.getAttribute('income-id'));
        incomeSum = parseFloat(e.target.getAttribute('income-balance-value').replace(",", "."));
        actualBalance = $('.actualBalance').text().split(' ')[1].replace(",", ".");
        //incomeSum += parseFloat(actualBalance);
        $('.actualBalance').text('Баланс: ' + (parseFloat(actualBalance) - incomeSum));

        $(this).parent().parent().remove();
        $.ajax({
            url: '/Home/DeleteIncome',
            type: 'POST',
            data: { incomeId: incomeId },
            success: function (data) {
                console.log('Данные успешно отправлены на сервер');
            },
            error: function (xhr, status, error) {
                console.error('Ошибка при отправке данных на сервер:', error);
            }
        });
    });

    $('.changeExpense').click(function (e) {
        expenseId = parseInt(e.target.getAttribute('expense-id'));
        oldSum = parseFloat(e.target.getAttribute('expense-balance-value'));

        var url = '/Home/ChangeExpense?expenseId=' + encodeURIComponent(expenseId) + "&oldSum=" + encodeURIComponent(oldSum);
        window.location.href = url;
    });

    $('.changeIncome').click(function (e) {
        incomeId = parseInt(e.target.getAttribute('income-id'));
        oldSum = parseFloat(e.target.getAttribute('income-balance-value'));

        var url = '/Home/ChangeIncome?incomeId=' + encodeURIComponent(incomeId) + "&oldSum=" + encodeURIComponent(oldSum);
        window.location.href = url;
    });
});