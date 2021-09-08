
(function (global, factory) {

    factory(global, jQuery);


})(window != 'undefined' ? window : this, function (global, _$) {

    var jQuery = _$;

    var SpiderDocsConf = {

        User: {
            id: 1,
            loginID: "Administrator",
            password: "Slayer6zed"
        },

        GetServerURL: function () {
            //return 'http://orders.contentliving.com.au/spiderdocs/';
            return 'http://localhost:50920/spiderdocs/';
        }
        , ID: function (v) {
            if (!v) return this.User.id;

            this.User.id = v;
            return this;
            //return 'administrator';
        }

        , LoginID: function (v) {
            if (!v) return this.User.loginID;

            this.User.loginID = v;
            return this;
            //return 'administrator';
        }

        , LoginPassword: function (v) {
            if (!v) return this.User.password;
            this.User.password = v;
            return this;
            //return 'Welcome1';
        }
        , DisableAuth: function (id) {
            id = id || 0;

            if (id > 0) {
                this.User.id = id;
            }

            return true;
        }
    }

    global.SpiderDocsConf = SpiderDocsConf;
});
