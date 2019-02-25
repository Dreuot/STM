window.addEventListener('load', function () {
    if (window.getRequest == undefined) {
        window.getRequest = function (url) {
            let request = new XMLHttpRequest();
            request.open('GET', location.origin + url, false);
            request.send(null);

            window.location.href = request.responseURL;
        }
    }
});

