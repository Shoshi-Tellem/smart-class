import React from 'react';
import { useForm, SubmitHandler } from 'react-hook-form';
import '../styles/Login.css'

interface LoginFormInputs {
    email: string;
    password: string;
}

const Login: React.FC = () => {
    const { register, handleSubmit, formState: { errors } } = useForm<LoginFormInputs>();

    const onSubmit: SubmitHandler<LoginFormInputs> = (data) => {
        console.log(data);
    };

    return (
        <div className="login-container">
            <h1 className="login-title">Login Form</h1>
            <form className="login-form" onSubmit={handleSubmit(onSubmit)}>
                <div className="form-group">
                    <label htmlFor="email" className="form-label">
                        Email:
                    </label>
                    <input 
                        id="email" 
                        type="email" 
                        className={`form-input ${errors.email ? 'input-error' : ''}`} 
                        {...register('email', { 
                            required: 'Email is required',
                            pattern: { 
                                value: /\S+@\S+\.\S+/,
                                message: 'Email format is invalid' 
                            }
                        })} 
                    />
                    {errors.email && <p className="error-message">{errors.email.message}</p>}
                </div>
                <div className="form-group">
                    <label htmlFor="password" className="form-label">
                        Password:
                    </label>
                    <input 
                        id="password" 
                        type="password" 
                        className={`form-input ${errors.password ? 'input-error' : ''}`} 
                        {...register('password', { 
                            required: 'Password is required',
                            minLength: { value: 6, message: 'Password must be at least 6 characters long' }
                        })} 
                    />
                    {errors.password && <p className="error-message">{errors.password.message}</p>}
                </div>
                <button type="submit" className="login-button">Login</button>
            </form>
        </div>
    );
};

export default Login;