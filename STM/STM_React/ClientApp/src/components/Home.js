import React, { Component } from 'react';

export class Home extends Component {
    static displayName = Home.name;

    render() {
        return (
            <div className="d-flex align-content-center flex-column" style={{ minWidth: '500px', maxWidth: '900px', margin: '0 auto' }}>
                <h1>Hello</h1>
                <h2>123123</h2>
                <h3>121245124512</h3>
                <h4>123125125152</h4>
                <h5>1231254zdfasf</h5>
                <h6>asdafaksdgflk</h6>
                <input type="text" className="stm-text" name="name" value="" placeholder="Простой текст" />
                <input type="text" className="stm-text error" name="name" value="" placeholder="Ошибка" />
                <input type="text" className="stm-text success" name="name" value="" placeholder="Успешно" />
                <input type="text" className="stm-text" name="name" value="" placeholder="Простой текст" />
                <input type="text" className="stm-text" disabled name="name" value="" placeholder="Отключено" />
                <div>
                    <div className="stm-checkbox">
                        <input id="check1" type="checkbox" />
                        <label for="check1">Текст чекбокса</label>
                    </div>
                </div>
                <div>
                    <div className="stm-checkbox">
                        <input id="check2" type="checkbox" />
                        <label for="check2">Текст чекбокса</label>
                    </div>
                </div>
                <div>
                    <div className="stm-checkbox">
                        <input id="check3" type="checkbox" />
                        <label for="check3">Текст чекбокса</label>
                    </div>
                </div>
                <div>
                    <div className="stm-radio">
                        <input name="test" id="radio1" type="radio" />
                        <label for="radio1">Текст радиокнопки</label>
                    </div>
                    <div className="stm-radio">
                        <input name="test" id="radio2" type="radio" />
                        <label for="radio2">Текст радиокнопки</label>
                    </div>
                    <div className="stm-radio">
                        <input name="test" id="radio3" type="radio" />
                        <label for="radio3">Текст радиокнопки</label>
                    </div>
                </div>
                <div className="d-flex">
                    <div>
                        <button className="stm-btn">Button</button>
                    </div>
                    <div>
                        <a className="stm-btn stm-btn-green" href="#">Button</a>
                    </div>
                    <div>
                        <a className="stm-btn stm-btn-blue" href="#">Button</a>
                    </div>
                    <div>
                        <a className="stm-btn stm-btn-yellow" href="#">Button</a>
                    </div>
                    <div>
                        <a className="stm-btn stm-btn-red" href="#">Button</a>
                    </div>
                    <div>
                        <a className="stm-btn stm-btn-purple" href="#">Button</a>
                    </div>
                    <div>
                        <a className="stm-btn stm-btn-gray" href="#">Button</a>
                    </div>
                    <div>
                        <a className="stm-btn stm-btn-light-gray" href="#">Button</a>
                    </div>
                    <div>
                        <a className="stm-btn stm-btn-dark-gray" href="#">Button</a>
                    </div>
                    <div>
                        <a className="stm-btn stm-btn-orange" href="#">Button</a>
                    </div>
                </div>
                <div>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Eligendi illum, id soluta mollitia quod necessitatibus rem deleniti, laborum non eius voluptate dolorum dolore qui saepe quisquam sed vero, nulla error?Lorem ipsum dolor sit amet, consectetur adipisicing elit. Eligendi illum, id soluta mollitia quod necessitatibus rem deleniti, laborum non eius voluptate dolorum dolore qui saepe quisquam sed vero, nulla error?Lorem ipsum dolor sit amet, consectetur adipisicing elit. Eligendi illum, id soluta mollitia quod necessitatibus rem deleniti, laborum non eius voluptate dolorum dolore qui saepe quisquam sed vero, nulla error?Lorem ipsum dolor sit amet, consectetur adipisicing elit. Eligendi illum, id soluta mollitia quod necessitatibus rem deleniti, laborum non eius voluptate dolorum dolore qui saepe quisquam sed vero, nulla error?Lorem ipsum dolor sit amet, consectetur adipisicing elit. Eligendi illum, id soluta mollitia quod necessitatibus rem deleniti, laborum non eius voluptate dolorum dolore qui saepe quisquam sed vero, nulla error?</div>
            </div>
        );
    }
}
