import React, { Component, useState } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchData } from './components/FetchData';
import { Counter } from './components/Counter';
import swal from 'sweetalert';
import $ from 'jquery';

export default class App extends Component {
    static displayName = App.name;
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <Layout>
                <Route exact path='/' component={Home} />
                <Route exact path='/Projects' component={Projects} />
                <Route path='/counter' component={Counter} />
                <Route path='/account/login' component={AccountLogin} />
                <Route path='/account/register' component={AccountRegister} />
                <Route path='/fetch-data' component={FetchData} />
            </Layout>
        );
    }
}

class Button extends Component {
    constructor(props) {
        super(props);
        this.state = {};
        //this.handleClick = this.handleClick.bind(this);
    }

    //handleClick() {
    //    this.props.onClick();
    //}

    render() {
        return (
            <a className={"stm-btn " + this.props.classNames} onClick={this.props.onClick}>{this.props.caption}</a>
        );
    }
}

class Text extends Component {
    constructor(props) {
        super(props);
        this.state = {
            value: this.props.onChange ? undefined : this.props.value,
            placeholder: this.props.placeholder,
            caption: this.props.caption
        };

        this.onChange = this.onChange.bind(this);
    }

    onChange = (e) => {
        this.setState({
            value: e.target.value,
        })
    }

    render() {
        return (
            <div className="d-flex align-items-center">
                <span className={"mr-1 text-nowrap " + (this.state.caption ? "" : "d-none")}>{this.state.caption}:</span>
                <input type={this.props.type ? this.props.type : "text"} onChange={this.props.onChange ? this.props.onChange : this.onChange} class={"stm-text " + this.props.classNames} name={this.props.name} placeholder={this.state.placeholder} value={this.state.value != undefined ? this.state.value : this.props.value} />
            </div>
        )
    }
}

class Popup extends Component {
    constructor(props) {
        super(props);
        this.onSubmit = this.onSubmit.bind(this);
        this.handleUserInput = this.handleUserInput.bind(this);
    }

    handleUserInput = (e) => {
        const name = e.target.name;
        const value = e.target.value;
        this.setState({ [name]: value });
    }

    onSubmit = (e) => {
        if (this.props.action) {
            let object = {};
            let formData = new FormData(e.target);
            formData.forEach(function (value, key) {
                object[key] = value;
            });


            if (this.props.addToSubmit) {
                object = this.props.addToSubmit(object);
            }

            var json = JSON.stringify(object);
            fetch(this.props.action, {
                method: this.props.method,
                body: json,
                headers: {
                    "Content-Type": "application/json",
                    "Accept": "application/json",
                    "Authorization": "Bearer " + sessionStorage.getItem("token"),
                },
            }).then(response => {
                if (!response.ok) {
                    response.text().then(message => swal({
                        title: "Ошибка",
                        text: message,
                        icon: "error",
                    }));
                    throw new Error("Breaked");
                }
            }).cath(message => console.log(message));
        }

        this.props.onOk();
    }

    render() {
        let dialog = (
            <div>
                <div class="stm-mask"></div>
                <div class="stm-popup-container">
                    <div class="stm-popup">
                        <div class="stm-popup-header">
                            {this.props.title}
                            <span class="stm-popup-close" onClick={this.props.onCancel}></span>
                        </div>
                        <form onSubmit={this.onSubmit} >
                            <div class="stm-popup-body">
                                {this.props.children}
                            </div>
                            <div class="stm-popup-footer d-flex justify-content-end">
                                <input type="submit" class="stm-btn ml-3 stm-btn-thin" value={this.props.okText ? this.props.okText : "Ок"} />
                                <button class="stm-btn ml-3 stm-btn-thin stm-btn-red" onClick={this.props.onCancel}>
                                    {this.props.cancelText ? this.props.cancelText : "Отмена"}
                                </button>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        );

        if (!this.props.isOpen)
            dialog = null;

        return (
            <div>
                {dialog}
            </div>
        )
    }
}

class Projects extends Component {
    constructor(props) {
        super(props);
        this.state = {
            projects: [],
            popupOpen: false,
            activeId: ""
        };

        this.reload = this.reload.bind(this);
        this.setActive = this.setActive.bind(this);
        this.reload();
    }

    setActive = (id) => {
        this.setState({
            activeId: id,
        })
    }

    reload = () => {
        let setActive = this.setActive;
        let activeId = this.state.activeId;
        fetch('api/Projects', {
            headers: {
                'Authorization': 'Bearer ' + sessionStorage.getItem('token'),
            }
        })
            .then(response => {
                if (response.redirected) {
                    window.location.href = response.url;
                    throw new Error('Breaked');
                }

                return response;
            })
            .then(response => {
                return response.json()
            })
            .then(json => {
                this.setState({
                    projects: json
                })
            })
            .catch((e) => console.log(e));
    }

    render() {
        let activeId = this.state.activeId;
        let setActive = this.setActive;
        let projects = this.state.projects.map((item) => <Project item={item} key={item.id} onClick={setActive} isActive={item.id == activeId} />);
        let reload = this.reload;
        let that = this;
        return (
            <div>
                <h3>
                    Проекты
                </h3>
                <div className="stm-table mb-3">
                    <div className="stm-table-header row">
                        <div className="col text-center">Название</div>
                        <div className="col text-center">Префикс</div>
                        <div className="col text-center">Описание</div>
                        <div className="col text-center">Ответственный</div>
                    </div>
                    {projects}
                </div>
                <Button caption="Добавить" onClick={() => this.setState({ popupOpen: true })} />
                <Button caption="Удалить" classNames="ml-3 stm-btn-red" onClick={function () {
                    if (!activeId) {
                        swal({
                            title: 'Необходимо выбрать запись',
                            icon: "warning"
                        });
                        return;
                    }

                    swal({
                        title: "Подтверждение удаления",
                        text: "Вы действительно хотите удалить выбранную запись?",
                        icon: "warning",
                        buttons: {
                            confirm: {
                                text: "Удалить",
                                value: true,
                                visible: true,
                                className: "",
                                closeModal: true
                            },
                            cancel: {
                                text: "Отмена",
                                value: null,
                                visible: true,
                                className: "",
                                closeModal: true,
                            },
                        }
                    }).then(function (result) {
                        if (result) {
                            fetch('api/projects/' + activeId, {
                                method: 'DELETE',
                                headers: {
                                    'Authorization': 'Bearer ' + sessionStorage.getItem('token'),
                                }
                            }).then(() => {
                                that.setState({ activeId: null });
                                reload();
                            });
                        }
                    });
                }}/>
                <Popup title="Создать проект" addToSubmit={(obj) => {
                    obj.manager = sessionStorage.getItem("id");

                    return obj;
                }} action="api/projects" method="POST" isOpen={this.state.popupOpen} onOk={(e) => {
                    this.setState({ popupOpen: false });
                    this.reload();
                }}
                    onCancel={(e) => {
                        this.setState({ popupOpen: false })
                    }}>
                    <Text placeholder="Название" name="name" value="" caption="Название" classNames="stm-text-plain" />
                    <Text placeholder="Префикс" name="prefix" value="" caption="Префикс" classNames="stm-text-plain" />
                    <Text placeholder="Описание" name="description" value="" caption="Описание" classNames="stm-text-plain" />
                </Popup>
            </div>
        );
    }
}

function Project(props) {
    let [id, setId] = useState(props.item.id);
    let [name, setName] = useState(props.item.name);

    let m = props.item.managerNavigation;
    let managerFIO = (m.lastName ? (m.lastName + ' ') : '') + (m.firstName ? (m.firstName + ' ') : '') + (m.midName ? m.midName : '');
    let [manager, setManager] = useState(managerFIO);

    let [prefix, setPrefix] = useState(props.item.prefix);
    let [description, setdescription] = useState(props.item.description);

    return (
        <div className={"row pb-2 pt-2 " + (props.isActive ? "stm-row-active" : "")} onClick={(e) => {
            props.onClick(id);
        }
        }>
            <div className="d-none">{id}</div>
            <div className="col text-center">{name}</div>
            <div className="col text-center">{prefix}</div>
            <div className="col text-center">{description}</div>
            <div className="col text-center">{manager}</div>
        </div>
    );
}

class AccountLogin extends Component {
    constructor(props) {
        super(props);
        this.state = {
            login: "",
            password: ""
        };
        this.handleUserInput = this.handleUserInput.bind(this);
        this.onSubmit = this.onSubmit.bind(this);
    }

    handleUserInput = (e) => {
        const name = e.target.name;
        const value = e.target.value;
        this.setState({ [name]: value });
    }

    onSubmit = (e) => {
        fetch("auth/login", {
            method: "POST",
            body: JSON.stringify(this.state),
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json",
                "Authorization": "Bearer " + sessionStorage.getItem("token"),
            },
        })
            .then(response => {
                if (response.ok) {
                    return response.json();
                }

                response.text().then(message => swal({
                    title: "Ошибка",
                    text: message,
                    icon: "error",
                }));
                throw new Error("Breaked");
            })
            .then(json => {
                sessionStorage.setItem("token", json.access_token);
                sessionStorage.setItem("id", json.userId);
                sessionStorage.setItem("user", json.username);
                window.location.href = window.location.origin;
            })
            .catch(error => {
                console.log(error.message);
            });

        e.preventDefault();
    }

    render() {
        return (
            <div>
                <div className="login-form mt-5">
                    <div className="d-flex justify-content-center">
                        <h4>Авторизация</h4>
                    </div>
                    <form onSubmit={this.onSubmit}>
                        <div className="row mb-3">
                            <div className="col">
                                <Text placeholder="Логин" name="login" value={this.state.login} onChange={this.handleUserInput} />
                            </div>
                        </div>
                        <div className="row mb-3">
                            <div className="col">
                                <Text placeholder="Пароль" name="password" type="password" value={this.state.value} onChange={this.handleUserInput} />
                            </div>
                        </div>
                        <div className="d-flex justify-content-center">
                            <input type="submit" className="stm-btn" value="Войти" />
                        </div>
                    </form>
                </div>

                <div className="login-form mt-3">
                    <div className="text-center">
                        У вас еще нет аккаунта? <a href="account/register" > зарегистрироваться</a>
                    </div>
                </div>
            </div>
        )
    }
}

class AccountRegister extends Component {
    constructor(props) {
        super(props);
        this.state = {
            login: "",
            password: "",
            firstName: "",
            lastName: "",
            email: "",
            confirmPassword: "",
        };
        this.handleUserInput = this.handleUserInput.bind(this);
        this.onSubmit = this.onSubmit.bind(this);
    }

    handleUserInput = (e) => {
        const name = e.target.name;
        const value = e.target.value;
        this.setState({ [name]: value });
    }

    onSubmit = (e) => {
        fetch("auth/register", {
            method: "POST",
            body: JSON.stringify(this.state),
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json",
                "Authorization": "Bearer " + sessionStorage.getItem("token"),
            },
        })
            .then(response => {
                if (response.ok) {
                    return response.json();
                }

                response.text().then(message => swal({
                    title: "Ошибка",
                    text: message,
                    icon: "error",
                }));
                throw new Error("Breaked");
            })
            .then(json => {
                sessionStorage.setItem("token", json.access_token);
                sessionStorage.setItem("id", json.userId);
                sessionStorage.setItem("user", json.username);
                window.location.href = window.location.origin;
            })
            .catch(error => {
                console.log(error.message);
            });

        e.preventDefault();
    }

    render() {
        return (
            <div class="login-form mt-5">
                <div class="d-flex justify-content-center">
                    <h4>Авторизация</h4>
                </div>
                <form onSubmit={this.onSubmit} >
                    <div class="row mb-3">
                        <div class="col">
                            <Text type="text" class="stm-text" name="login" value={this.state.login} onChange={this.handleUserInput} placeholder="Логин" />
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col">
                            <Text type="password" class="stm-text" name="password" value={this.state.password} onChange={this.handleUserInput} placeholder="Пароль" />
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col">
                            <Text type="password" class="stm-text" name="confirmPassword" value={this.state.confirmPassword} onChange={this.handleUserInput} placeholder="Подтвердите пароль" />
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col">
                            <Text type="text" class="stm-text" name="email" value={this.state.email} onChange={this.handleUserInput} placeholder="Email" />
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col">
                            <Text type="text" class="stm-text" name="firstName" value={this.state.firstName} onChange={this.handleUserInput} placeholder="Имя" />
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col">
                            <Text type="text" class="stm-text" name="lastName" value={this.state.lastName} onChange={this.handleUserInput} placeholder="Фамилия" />
                        </div>
                    </div>
                    <div class="d-flex justify-content-center">
                        <input type="submit" class="stm-btn" value="Зарегистрироваться" />
                    </div>
                </form>
            </div>
        )
    }
}