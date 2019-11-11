class HandlebarsHelper {
    constructor() {
    }

    registStringEqual() {
        Handlebars.registerHelper('StringEqual', function (x1, x2, options) {
            if (x1 === x2) {
                return options.fn(this);
            } else {
                return options.inverse(this);
            }
        });
    }

    registStringNotEqual() {
        Handlebars.registerHelper('StringNotEqual', function (x1, x2, options) {
            if (x1 === x2) {
                return options.inverse(this);
            } else {
                return options.fn(this);
            }
        });
    }
}
