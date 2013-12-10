messageBox = function (title, message, buttons) {
    /// <summary>Mostra uma caixa de mensagem para o usuário.</summary>
    /// <param name="title" type="string">O título da caixa de mensagem.</param>
    /// <param name="message" type="string">A mensagem que será apresentada na caixa.</param>
    /// <param name="buttons" type="array">Array de botões que serão visualizados na caixa. 
    // Seguem o formato { 'Texto do botão': function() { // ação que será executada ao clicar no botão } }.
    // [
    // 				{ id:'confirmYesButton', 'title': globalConfirmYes, 'action': callback },
    //				{ id:'confirmNoButton', 'title': globalConfirmNo, 'action': '', 'mode': 'info|success|warning|danger' }
    // ]
    // </param>

    var renderModal = function () {
        if (title === undefined || title === null) {
            $('.modal-header').hide();
        } else {
            $("#modalMessageBoxTitle").html(title);
        }

        $("#modalMessageBoxMessage").html(message);
        $("#modalMessageBoxButtons").html("");

        for (var i in buttons) {
            var b = buttons[i];
            var newButton = $('#modalMessageBoxButtons').append('<button id="modalMessageBoxYesButton" class="btn" data-dismiss="modal">' + b.title + '</button>').children().last();
            newButton.attr("id", b.id);
            newButton.click(b.action);

            if (b.mode) {
                newButton.addClass('btn-' + b.mode);
            }
            else {
                newButton.addClass('btn-success');
            }

            newButton.hide();
            newButton.fadeIn(i * 700);
        }

        $('#modalMessageBox').modal();
    };

    // Carrega o html da modal na página, caso ainda não tenha sido carregado.
    if ($("#modalMessageBox").length === 0) {
        $.ajax({
            url: '/Scripts/Framework/MessageBox/MessageBoxModal.html',
            success: function (data) {
                $('body').append(data);
                renderModal();
            },
            dataType: 'html'
        });
    }
    else {
        renderModal();
    }
};

alert = function (message, title) {
    messageBox(title, message,
    [
        { id: 'alertYesButton', 'title': globalization.texts.ok, 'mode': 'success', 'action': function () { } }
    ]);
};

confirm = function (message, yesCallback, noCallback, title) {
    messageBox(title, message,
    [
        { id: 'confirmYesButton', 'title': globalization.texts.yes, 'mode': 'success', 'action': yesCallback },
        { id: 'confirmNoButton', 'title': globalization.texts.no, 'mode': 'danger', 'action': noCallback }
    ]);
};