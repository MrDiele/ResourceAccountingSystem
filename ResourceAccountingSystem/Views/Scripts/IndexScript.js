function formatItem(item) {
    var Counter = '';
    if (item.SerialNumber === null) {
        item.indication = '';
        item.SerialNumber = '';
    } else {
        Counter = ': $' + item.SerialNumber + ': $' + item.Indication;
    }
    return item.IdHouse + ': $' + item.Address + Counter;
}   

function del(id, o) {
    $.ajax({
        type: 'DELETE',
        url: "api/Houses/" + id,
        dataType: 'json',
        success: function () {
            var p = o.parentNode.parentNode;
            p.parentNode.removeChild(p);
        },
        error: function (xhr, textStatus, errorThrown) {
            console.log('Error in Operation');
        }
    });
}   

function addCounter() {
    var house = new Object();
    house.Counters = [{ SerialNumber: $('#SerialNumberWhenAddCounter').val(), Indication: $('#IndicatinWhenAddCounter').val() }];
    house.IdHouse = $('#IdHouseWhenAddCounter').val();
    $.ajax({
        type: 'PUT',
        url: "/api/Houses/" + $('#IdHouseWhenAddCounter').val(),
        dataType: 'json',
        data: house,
        success: function (data, textStatus, xhr) {
            updateDateOnPage();
        },
        error: function (xhr, textStatus, errorThrown) {
            console.log('Error in Operation');
        }
    });
} 

function InputIndicationBySerialNumber() {
    var counter = new Object();
    counter.SerialNumber = $('#Counter_SerialNumber').val();
    counter.Indication = $('#Counter_Indication').val();
    $.ajax({
        type: 'PUT',
        url: "api/Counters/",
        dataType: 'json',
        data: counter,
        success: function (data, textStatus, xhr) {
            $('#fail_inputCS').text("");
            updateDateOnPage();
        },
        error: function (xhr, textStatus, errorThrown) {
            console.log('Error in Operation');
            $('#fail_inputCS').text("Ошибка в момент внесения показаний в систему");
        }
    });
}

function InputIndicationByIdHouse() {
    var house = new Object();
    house.Counters = [{ Indication: $('#House_Indication').val() }];
    house.IdHouse = $('#House_IdHouse').val();
    $.ajax({
        type: 'PUT',
        url: "api/Houses/inputIndication",
        dataType: 'json',
        data: house,
        success: function (data, textStatus, xhr) {
            $('#fail_inputIH').text("");
            updateDateOnPage();
        },
        error: function (xhr, textStatus, errorThrown) {
            console.log('Error in Operation');
            $('#fail_inputIH').text("Ошибка в момент внесения показаний в систему");
        }
    });
}
