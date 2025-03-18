import React from 'react';
import { useForm, SubmitHandler } from 'react-hook-form';
import '../styles/Registration.css'

interface RegistrationFormInputs {
    institutionName: string;
    adminName: string;
    adminEmail: string;
    password: string;
}

const Registration: React.FC = () => {
    const { register, handleSubmit, formState: { errors } } = useForm<RegistrationFormInputs>();

    const onSubmit: SubmitHandler<RegistrationFormInputs> = (data) => {
        console.log(data);
        // כאן תוכל להוסיף לוגיקה לשליחת הנתונים לשרת
    };

    return (
        <div className="registration-container">
            <h1 className="registration-title">Registration Form</h1>
            <form className="registration-form" onSubmit={handleSubmit(onSubmit)}>
                <div className="form-group">
                    <label htmlFor="institutionName" className="form-label">
                        Institution Name:
                    </label>
                    <input 
                        id="institutionName" 
                        type="text" 
                        className={`form-input ${errors.institutionName ? 'input-error' : ''}`} 
                        {...register('institutionName', { 
                            required: 'Institution name is required',
                            minLength: { value: 3, message: 'Institution name must be at least 3 characters long' }
                        })} 
                    />
                    {errors.institutionName && <p className="error-message">{errors.institutionName.message}</p>}
                </div>
                <div className="form-group">
                    <label htmlFor="adminName" className="form-label">
                        Admin Name:
                    </label>
                    <input 
                        id="adminName" 
                        type="text" 
                        className={`form-input ${errors.adminName ? 'input-error' : ''}`} 
                        {...register('adminName', { 
                            required: 'Admin name is required',
                            minLength: { value: 3, message: 'Admin name must be at least 3 characters long' }
                        })} 
                    />
                    {errors.adminName && <p className="error-message">{errors.adminName.message}</p>}
                </div>
                <div className="form-group">
                    <label htmlFor="adminEmail" className="form-label">
                        Admin Email:
                    </label>
                    <input 
                        id="adminEmail" 
                        type="email" 
                        className={`form-input ${errors.adminEmail ? 'input-error' : ''}`} 
                        {...register('adminEmail', { 
                            required: 'Admin email is required', 
                            pattern: { 
                                value: /\S+@\S+\.\S+/,
                                message: 'Email format is invalid' 
                            }
                        })} 
                    />
                    {errors.adminEmail && <p className="error-message">{errors.adminEmail.message}</p>}
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
                <button type="submit" className="registration-button">Register</button>
            </form>
        </div>
    );
};

export default Registration;