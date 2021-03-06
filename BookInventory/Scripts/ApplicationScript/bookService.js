﻿$(function () {
    $("#isbnLookup1").click(function () {
        var isbn1 = $('#Book_Isbn10').val();
        if (isbn1 !== "") {
            ajaxCall(isbn1);
        }
        else {            
            alert("Please enter an Isbn10 number");
        }
    })
    
    function ajaxCall(isbn) {
        //clear all textbox before call
        $("input[type=text]").val("");
        $("textarea").val("");

        // make the call
        $.ajax({
            type: "GET",
            url: "https://www.googleapis.com/books/v1/volumes",
            data: { q: 'isbn:' + isbn },
            success: function (response) {
                //what to do after the call is success
                if (response.items === undefined) {
                    alert("Sorry, cannot find any book with this Isbn number");
                }
                else {
                    //fill in all textboxes with results
                    var itemFound = response.items[0];

                    //check ISBN number in the first item found to see its Isbn numbers are valid
                    var volumeInfo = itemFound.volumeInfo;
                    var industryIdentifiers = volumeInfo.industryIdentifiers;
                    if (industryIdentifiers[0].type === 'ISBN_13') {
                        var Isbn13 = industryIdentifiers[0].identifier;
                        var Isbn10 = industryIdentifiers[1].identifier;
                        $('#Book_Isbn10').val(Isbn10);
                        $('#Book_Isbn13').val(Isbn13);
                    }
                    else if (industryIdentifiers[0].type === 'ISBN_10') {
                        var Isbn13 = industryIdentifiers[1].identifier;
                        var Isbn10 = industryIdentifiers[0].identifier;
                        $('#Book_Isbn10').val(Isbn10);
                        $('#Book_Isbn13').val(Isbn13);
                    }

                    if (volumeInfo.imageLinks !== undefined) {
                        var imageLinks = volumeInfo.imageLinks;
                        var thumbnail = imageLinks.thumbnail;
                        if (thumbnail !== undefined) {
                            $('#bookThumbnail').attr('src', thumbnail);
                            $('#Book_CoverThumbnailPath').val(thumbnail);
                        }
                    }
                    else {
                        $('#Book_CoverThumbnailPath').val($('#bookThumbnail').attr('src'));
                    }

                    var title = volumeInfo.title;
                    $('#Book_Title').val(title);
                    var subtitle = volumeInfo.subtitle;
                    $('#Book_Subtitle').val(subtitle);
                    var publisher = volumeInfo.publisher;
                    $('#Book_Publisher').val(publisher);
                    var publishedDate = volumeInfo.publishedDate;
                    $('#Book_PublishedDate').val(publishedDate);
                    var desc = volumeInfo.description;
                    $('#Book_Description').val(desc);
                    var authors = volumeInfo.authors;
                    $('#AuthorsText').val(authors);
                    var categories = volumeInfo.categories;
                    $('#CategoriesText').val(categories);

                }
                console.log(response);
            },
            error: function (exception) {
                //something bad happened
                //debugger
                alert(exception);
            }
        });
    }
});
