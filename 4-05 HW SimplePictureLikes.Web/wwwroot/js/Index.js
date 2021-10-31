$(() => {

    setInterval(() => {
        updateLikes();
    }, 500)
        function updateLikes() {
            const id = $("#image-id").val();
            $.get('/home/getlikes', { id }, function (result) {
                $("#likes-count").text(result.likes);
            });
    }
    $("#like-button").on('click', function () {
        const id = $("#image-id").val();
        $.post('/home/like', { id }, function () {
            console.log("hi")
            updateLikes();
            $("#like-button").prop('disabled', true);
        });
    })
})