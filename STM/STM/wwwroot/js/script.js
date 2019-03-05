window.addEventListener('load', function () {
    if (window.getRequest == undefined) {
        window.getRequest = function (url) {
            let request = new XMLHttpRequest();
            request.open('GET', location.origin + url, false);
            request.send(null);

            return request;
        }
    }
});


if (!window.showPopup) {
    window.showPopup = function (header, innerHtml, okConfig, cancelConfig) {
        let mask = document.createElement('div');
        mask.classList.add('stm-mask');

        let container = document.createElement('div');
        container.classList.add('stm-popup-container');

        let popup = document.createElement('div');
        popup.classList.add('stm-popup');

        let popupHeader = document.createElement('div');
        popupHeader.classList.add('stm-popup-header');
        popupHeader.innerText = header;

        let closePopup = function () {
            let mask = document.getElementsByClassName('stm-mask');
            for (let i = 0; i < mask.length; i++) {
                document.body.removeChild(mask[i])
            }

            let container = document.getElementsByClassName('stm-popup-container');
            for (let i = 0; i < container.length; i++) {
                document.body.removeChild(container[i])
            }
        };

        let close = document.createElement('span');
        close.classList.add('stm-popup-close');
        close.addEventListener('click', function (e) {
            closePopup();
        });

        popupHeader.appendChild(close);

        let popupBody = document.createElement('div');
        popupBody.classList.add('stm-popup-body');

        let popupFooter = document.createElement('div');
        popupFooter.classList.add('stm-popup-footer');
        popupFooter.classList.add('d-flex');
        popupFooter.classList.add('justify-content-end');

        if (okConfig) {
            let form = this.document.createElement('form');
            form.action = okConfig.action ? okConfig.action : '#';
            form.method = okConfig.method ? okConfig.method : 'get';

            let ok = document.createElement('input');
            ok.setAttribute('type', 'submit');
            ok.classList.add(okConfig.class ? okConfig.class : 'stm-btn');
            ok.classList.add('ml-3');
            ok.classList.add('stm-btn-thin');

            ok.value = okConfig.text ? okConfig.text : 'OK';

            form.appendChild(ok);
            popupFooter.appendChild(form);
        }

        let cancel = document.createElement('button');
        cancel.classList.add('stm-btn')
        cancel.classList.add('ml-3');
        cancel.classList.add('stm-btn-thin');
        cancel.innerText = 'Cancel';

        if (cancelConfig) {
            cancel.classList.add(cancelConfig.class ? cancelConfig.class : 'stm-btn-red');
            cancel.innerText = cancelConfig.text ? okConfig.text : 'Cancel';
        }

        cancel.addEventListener('click', function () {
            closePopup();
        })

        popupFooter.appendChild(cancel);

        popupBody.innerHTML = innerHtml;
        popup.appendChild(popupHeader);
        popup.appendChild(popupBody);
        popup.appendChild(popupFooter);

        container.appendChild(popup);
        this.document.body.appendChild(mask);
        this.document.body.appendChild(container);
    }
}

if (!window.ShowPartialViewPopup) {
    window.ShowPartialViewPopup = function (title, action, ok, cancel) {
        showPopup(title, getRequest(action).responseText, ok, cancel);
    }
}
