function remove(url,id,e) {
    var btn = $(e);
    bootbox.confirm("Êtes-vous sûr de vouloir supprimer?", function (ok) {
        if (ok) {
            $.ajax({
                type: "POST",
                url: url,
                data: { id: id },
                success: function () {
                    location.reload();
                },
                error: function (er) {
                    console.log(er);
                }
            });
        }
    });
}