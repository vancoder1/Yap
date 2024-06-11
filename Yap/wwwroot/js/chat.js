(function ($, window, document) {
    'use strict';

    var chatApp = {
        init: function () {
            this.cacheDOM();
            this.bindEvents();
            this.setupSignalR();
        },
        cacheDOM: function () {
            this.$messageList = $('#messagesList');
            this.$sendButton = $('#sendButton');
            this.$userInput = $('#userInput');
            this.$messageInput = $('#messageInput');
            this.$sendButton.prop('disabled', true);
        },
        bindEvents: function () {
            this.$sendButton.on('click', this.sendMessage.bind(this));
        },
        setupSignalR: function () {
            this.connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

            this.connection.on("ReceiveMessage", this.receiveMessage.bind(this));

            this.connection.start().then(this.connectionEstablished.bind(this)).catch(this.connectionError);
        },
        connectionEstablished: function () {
            this.$sendButton.prop('disabled', false);
            this.$messageInput.focus();
        },
        connectionError: function (err) {
            console.error(err.toString());
        },
        sendMessage: function (event) {
            event.preventDefault();
            var user = this.$userInput.val();
            var message = this.$messageInput.val();
            if (message) {
                this.connection.invoke("SendMessage", user, message).catch(this.connectionError);
                this.$messageInput.val('').focus();
            }
        },
        receiveMessage: function (user, message) {
            var $li = $('<li/>').text(`${user} says ${message}`);
            this.$messageList.append($li);
        }
    };

    $(document).ready(function () {
        chatApp.init();
    });

}(jQuery, window, document));