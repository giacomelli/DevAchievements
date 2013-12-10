(function () {
    window.globalization = {
        getText: function (key) {
            var result = null;

            $.each(globalization.texts, function (index, value) {
                if (result === null && index.toLowerCase() == key.toLowerCase()) {
                    result = value;
                    return false;
                }
            });

            if (result === null && console) {
                console.log("[TEXT NOT FOUND] {0}".format(key));
            }

            return result;
        },
        join: function (valuesArray) {
            
            var result = valuesArray.join(", ");

            if(valuesArray.length > 1) {
                result = result.substring(0, result.lastIndexOf(", ")) + " " + globalization.texts.and + " " + result.substring(result.lastIndexOf(", ") + 2);
            }
            
            return result;
        }
    };
})();
