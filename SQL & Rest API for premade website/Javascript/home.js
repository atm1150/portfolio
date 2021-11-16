$(document).ready(function () {
    LoadDvds();

    $('#createDVDButton').click(function (event) {
        HideMainPage();
        $('#addDvdForm').show();
    });

    $('#addDvdButton').click(function (event) {

        $('#errorMessages').empty();

        var haveValidationErrors = checkAndDisplayValidationErrors($('#addDvdForm').find('input'));
        if (haveValidationErrors) {
            return false;
        }

        $.ajax({
            type: 'POST',
            url: 'https://localhost:44363/dvd',
            data: JSON.stringify({
                title: $('#addTitle').val(),
                releaseYear: $('#addYear').val(),
                director: $('#addDirector').val(),
                rating: $('#addRating').val(),
                notes: $('#addNotes').val()
            }),
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            'dataType': 'json',
            success: function (data, status) {
               /* // clear errorMessages
                $('#errorMessages').empty();
                // Clear the form and reload the table
                $('#addTitle').val('');
                $('#addYear').val('');
                $('#addDirector').val('');
                $('#addRating').val('');
                $('#addNotes').val('');
                */

                HideAddForm();
                LoadDvds();
            },
            error: function () {
                $('#errorMessages')
                    .append($('<li>')
                        .attr({ class: 'list-group-item list-group-item-danger' })
                        .text('Error calling web service.  Please try again later.'));
            }
        });
    });



    $('#searchButton').click(function (event) {

        $('#errorMessages').empty();
        var choice = $('#searchCategory').val();
        var searchTerm = $('#searchTermBox').val();

        var haveValidationErrors = checkAndDisplayValidationErrors($('#searchForm').find('input'));
        if (haveValidationErrors) {
            return false;
        }

        if (choice == null) {
            return false;
        }

        ClearDvdTable();

        var contentRows = $('#contentRows');

        $.ajax({
            type: 'GET',
            url: 'https://localhost:44363/dvds/' + choice + '/' + searchTerm,
            success: function (data, status) {
                $.each(data, function (index, dvd) {
                    var id = dvd.id;
                    var title = dvd.title;
                    var releaseYear = dvd.releaseYear;
                    var director = dvd.director;
                    var rating = dvd.rating;
                    var notes = dvd.notes;

                    var row = '<tr>';
                    row += '<td><a onclick="showDvdDetails(' + id + ')">' + title + '</a></td>';
                    row += '<td>' + releaseYear + '</td>';
                    row += '<td>' + director + '</td>';
                    row += '<td>' + rating + '</td>';
                    row += '<td><button type="button" class="btn btn-info" onclick="ShowEditForm(' + id + ')">Edit</button></td>';
                    row += '<td><button type="button" class="btn btn-danger" onclick="DeleteDvd(' + id + ')">Delete</button></td>';
                    row += '</tr>';
                    contentRows.append(row);
                });
            },
            error: function () {
                $('#errorMessages')
                    .append($('<li>')
                        .attr({ class: 'list-group-item list-group-item-danger' })
                        .text('Error calling web service.  Please try again later.'));
            }
        });
    });

    $('#backButton').click(function (event) {
        $('#dvdDetailsHeader').text('');
        $('#dvdDetails').text('');
        $('#dvdDetailsDiv').hide();
        $('#navHeader').show();
        $('#dvdListDiv').show();
    });
});

function CreateEditButton(dvdId) {
    $('#editDvdButton').click(function (event) {

        $('#errorMessages').empty();

        var haveValidationErrors = checkAndDisplayValidationErrors($('#editDvdForm').find('input'));
        if (haveValidationErrors) {
            return false;
        }
        $.ajax({
            type: 'PUT',
            url: 'https://localhost:44363/dvd/' + dvdId,
            data: JSON.stringify({
                id: dvdId,
                title: $('#editTitle').val(),
                releaseYear: $('#editYear').val(),
                director: $('#editDirector').val(),
                rating: $('#editRating').val(),
                notes: $('#editNotes').val()
            }),
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            'dataType': 'json',
            success: function () {
                
                HideEditForm();
                LoadDvds();
            },
            error: function () {
                $('#errorMessages')
                    .append($('<li>')
                        .attr({ class: 'list-group-item list-group-item-danger' })
                        .text('Error calling web service.  Please try again later.'));
            }
        })

    });
}

function HideMainPage() {
    $('#navHeader').hide();
    $('#dvdListDiv').hide();
}

function ShowMainPage() {
    $('#navHeader').show();
    $('#dvdListDiv').show();
}

function HideEditForm() {
    $('#errorMessages').empty();
    $('#addTitle').val('');
    $('#addYear').val('');
    $('#addDirector').val('');
    $('#addRating').val('');
    $('#addNotes').val('');

    ShowMainPage();

    $('#editDvdForm').hide();
}

function HideAddForm() {
    $('#errorMessages').empty();

    $('#addTitle').val('');
    $('#addYear').val('');
    $('#addDirector').val('');
    $('#addRating').val('');
    $('#addNotes').val('');

    $('#addDvdForm').hide();
    ShowMainPage();
    
}

function CreateErrorMessage() {
    $('#errorMessages')
        .append($('<li>')
            .attr({ class: 'list-group-item list-group-item-danger' })
            .text('Error calling web service.  Please try again later.'));
}

function ClearErrorMessages() {
    $('#errorMessages').empty();
}

function ClearDvdTable() {
    $('#contentRows').empty();
}

function LoadDvds() {
    ClearDvdTable();

    var contentRows = $('#contentRows');

    $.ajax({
        type: 'GET',
        url: 'https://localhost:44363/dvds',
        success: function (dvdArray) {
            $.each(dvdArray, function (index, dvd) {
                var id = dvd.id;
                var title = dvd.title;
                var releaseYear = dvd.releaseYear;
                var director = dvd.director;
                var rating = dvd.rating;
                var notes = dvd.notes;

                var row = '<tr>';
                row += '<td><a onclick="showDvdDetails(' + id + ')">' + title + '</a></td>';
                row += '<td>' + releaseYear + '</td>';
                row += '<td>' + director + '</td>';
                row += '<td>' + rating + '</td>';
                row += '<td><button type="button" class="btn btn-info" onclick="ShowEditForm(' + id + ')">Edit</button></td>';
                row += '<td><button type="button" class="btn btn-danger" onclick="DeleteDvd(' + id + ')">Delete</button></td>';
                row += '</tr>';

                contentRows.append(row);
            });
        },
        error: function () {
            CreateErrorMessage();
        }
    })
}

function ShowEditForm(dvdId) {
    ClearErrorMessages();
    $.ajax({
        type: 'GET',
        url: 'https://localhost:44363/dvd/' + dvdId,
        success: function (dvd) {
            $('#editHeader').append(dvd.title);
            $('#editTitle').val(dvd.title);
            $('#editYear').val(dvd.releaseYear);
            $('#editDirector').val(dvd.director);
            $('#editRating').val(dvd.rating);
            $('#editNotes').val(dvd.notes);
            $('#editDvdId').val(dvd.id)
        },
        error: function () {
            CreateErrorMessage();
        }
    });
    CreateEditButton(dvdId);
    HideMainPage();
    $('#editDvdForm').show();
}

function ShowDvdDetails(dvdId) {
    ClearErrorMessages();
    HideMainPage();

    $.ajax({
        type: 'GET',
        url: 'https://localhost:44363/dvd/' + dvdId,
        success: function (dvd) {
            var id = dvd.id;
            var title = dvd.title;
            var releaseYear = dvd.releaseYear;
            var director = dvd.director;
            var rating = dvd.rating;
            var notes = dvd.notes;

            var cell = '<p>' + releaseYear + '</p>';
            cell += '<p>' + director + '</p>';
            cell += '<p>' + rating + '</p>';
            cell += '<p>' + notes + '</p>';

            $('#dvdDetailsHeader').append(title);
            $('#dvdDetails').append(cell);
            $('#dvdDetailsDiv').show();
        },
        error: function () {
            CreateErrorMessage();
        }
    });
}

function DeleteDvd(dvdId) {
    var response = confirm("Are you sure you want to delete this DVD from your collection?");
    if (response == true) {

        $.ajax({
            type: 'DELETE',
            url: 'https://localhost:44363/dvd/' + dvdId,
            success: function (status) {
                ClearDvdTable();
                LoadDvds();
            },
            error: function () {
                $('#errorMessages')
                    .append($('<li>')
                        .attr({ class: 'list-group-item list-group-item-danger' })
                        .text('Error calling web service.  Please try again later.'));
            }
        });
    }

    
}

function checkAndDisplayValidationErrors(input) {

    $('#errorMessages').empty();
    var errorMessages = [];

    input.each(function () {
        if (!this.validity.valid) {
            switch ($(this).attr('id')) {
                case 'searchTermBox':
                    this.setCustomValidity('Both Search Category and Search Term are required.');
                    break;
                case 'searchCategory':
                    this.setCustomValidity('Both Search Category and Search Term are required.');
                    break;
                case 'editYear':
                    this.setCustomValidity('Please enter a 4-digit year.');
                    break;
                case 'editTitle':
                    this.setCustomValidity('Please enter a title for the DVD.');
                    break;
                case 'addYear':
                    this.setCustomValidity('Please enter a 4-digit year.');
                    break;
                case 'addTitle':
                    this.setCustomValidity('Please enter a title for the DVD.');
                    break;
                default:
            }
            var errorField = $('label[for=' + this.id + ']').text();
            errorMessages.push(errorField + ' ' + this.validationMessage);
        }
    });

    if (errorMessages.length > 0) {
        $.each(errorMessages, function (index, message) {
            $('#errorMessages').append($('<li>').attr({ class: 'list-group-item list-group-item-danger' }).text(message));
        });
        return true;
    } else {
        return false;
    }
}