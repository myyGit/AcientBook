var vue = new Vue({
    el: "#register",
    data: {
        sendBtnDisabled: false,
        sendBtnClass: "btn-success",
        message:"发送验证码"
    },
    methods: {
        semdMessageWait: function () {
            this.sendBtnClass = "btn-default";
            this.message = "60秒后重发";
            this.sendBtnDisabled = "disabled";
        }
    }
})  