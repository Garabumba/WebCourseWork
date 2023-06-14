////let now = new Date();

////$("#prevMonth").click(function () {
////    now.setMonth(now.getMonth() - 1);
////    updateMonth();
////});

////$("#nextMonth").click(function () {
////    now.setMonth(now.getMonth() + 1);
////    updateMonth();
////});

////function updateMonth() {
////    const monthNames = [
////        "January", "February", "March", "April", "May", "June", "July",
////        "August", "September", "October", "November", "December"
////    ];

////    const year = now.getFullYear();
////    const month = monthNames[now.getMonth()];
////    var url = '/Statistic/GetStatistic?month=' + encodeURIComponent(month) + "&year=" + encodeURIComponent(year);
////    window.location.href = url;
////    $("#month").text(month + " " + year);

    //$.ajax({
    //    url: '/Statistic/GetStatistic',
    //    type: 'GET',
    //    data: { month: month, year: year },
    //    success: function (data) {
    //        console.log('Данные успешно отправлены на сервер');
    //    },
    //    error: function (xhr, status, error) {
    //        console.error('Ошибка при отправке данных на сервер:', error);
    //    }
    //});
    
//}

//updateMonth();

let now = new Date();

var names = document.querySelectorAll('.nav-link');
names[0].classList.remove("active");
names[1].classList.remove("active");
names[2].classList.add("active");

$("#prevMonth").click(function () {
    now.setMonth(now.getMonth() - 1);
    updateMonth();
});

$("#nextMonth").click(function () {
    now.setMonth(now.getMonth() + 1);
    updateMonth();
});

function updateMonth() {
    const monthNames = [
        "Январь", "Февраль", "Март", "Апрель", "Май", "Июнь", "Июль",
        "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь"
    ];

    const year = now.getFullYear();
    const monthNumber = now.getMonth() + 1;
    const monthName = monthNames[now.getMonth()];

    var url = '/Statistic/GetStatistic?month=' + encodeURIComponent(monthNumber) + "&year=" + encodeURIComponent(year);

    // Отправляем AJAX-запрос типа GET по указанному URL
    $.get(url, function (data) {
        $("#month").text(monthName + " " + year);

        var tempDiv = document.createElement('div');
        tempDiv.innerHTML = data;
        var expenseList = tempDiv.querySelector('#expenseList');
        var incomeList = tempDiv.querySelector('#incomeList');
        
        $("#expenseList").html(expenseList);
        $("#incomeList").html(incomeList);

        var expensesSum = document.querySelectorAll("#expenseSum");
        var incomesSum = document.querySelectorAll("#incomeSum");

        var totalExpenseSum = 0;
        var totalIncomeSum = 0;

        expensesSum.forEach(x => totalExpenseSum += parseFloat(x.textContent.replace(",", ".")));
        incomesSum.forEach(x => totalIncomeSum += parseFloat(x.textContent.replace(",", ".")));
        console.log(totalExpenseSum);
        console.log(totalIncomeSum);

        $("#expensesSum").text(`Расходы: ${totalExpenseSum}`);
        $("#incomesSum").text(`Доходы: ${totalIncomeSum}`);
        $("#total").text(`Итого: ${totalIncomeSum - totalExpenseSum}`);

        

        history.pushState(null, '', url);

        drawCircle(totalExpenseSum, totalIncomeSum);
    })
        .fail(function (xhr, status, error) {
            // Обработка ошибки
            console.log("Ошибка при выполнении AJAX-запроса:", error);
        });
}

updateMonth();



function drawCircle(eSum, iSum) {
    var data = [
        { label: 'Расходы', value: eSum },
        { label: 'Доходы', value: iSum }
    ];

    // Цвета для секторов диаграммы (пример)
    var colors = ['#ff6384', '#36a2eb', '#ffce56'];

    // Получение контекста канваса
    var canvas = document.getElementById('pieChart');
    var context = canvas.getContext('2d');

    // Установка размеров канваса
    canvas.width = canvas.offsetWidth;
    canvas.height = canvas.offsetHeight;

    // Расчет суммы значений для определения процентного соотношения
    var totalValue = data.reduce(function (sum, item) {
        return sum + item.value;
    }, 0);

    // Установка начального угла и конца угла для каждого сектора
    var startAngle = 0;
    var endAngle = 0;

    // Отрисовка каждого сектора диаграммы
    data.forEach(function (item, index) {
        // Расчет конечного угла для текущего сектора
        endAngle = startAngle + (item.value / totalValue) * (2 * Math.PI);

        // Нарисовать сектор
        context.beginPath();
        context.moveTo(canvas.width / 2, canvas.height / 2);
        context.arc(
            canvas.width / 2,
            canvas.height / 2,
            Math.min(canvas.width, canvas.height) / 2 - 20,
            startAngle,
            endAngle
        );
        context.closePath();
        context.fillStyle = colors[index % colors.length];
        context.fill();

        // Обновить начальный угол для следующего сектора
        startAngle = endAngle;
    });

    // Добавление легенды
    var legend = document.getElementById('legend');
    if (legend.childElementCount == 0)
        data.forEach(function (item, index) {
            var label = document.createElement('span');
            label.style.backgroundColor = colors[index % colors.length];
            var labelText = document.createTextNode(item.label);
            label.appendChild(labelText);
            legend.appendChild(label);
        });
}