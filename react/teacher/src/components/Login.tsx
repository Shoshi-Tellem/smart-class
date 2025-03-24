import React, { useState } from 'react';
import { useAppDispatch, useAppSelector } from '../stores/store';
import { useForm } from 'react-hook-form';
import { TextField, Button, CircularProgress, Typography, Box } from '@mui/material';
import { loginUser, changePassword } from '../stores/authStore';
import { useNavigate } from 'react-router-dom';

interface LoginFormInputs {
    email: string;
    password: string;
}

interface ChangePasswordInputs {
    newPassword: string;
}

interface LoginResult {
    needsPasswordChange: boolean; // שדה לדוגמה, הוסף שדות נוספים לפי הצורך
}

interface LoginProps {
    onLogin: () => void;
}

const Login: React.FC<LoginProps> = ({ onLogin }) => {
    const dispatch = useAppDispatch();
    const navigate = useNavigate();
    const { register, handleSubmit, formState: { errors } } = useForm<LoginFormInputs>();
    const { register: registerChangePassword, handleSubmit: handleSubmitChangePassword, formState: { errors: errorsChangePassword } } = useForm<ChangePasswordInputs>();
    const loading = useAppSelector((state) => state.auth.loading);
    const error = useAppSelector((state) => state.auth.error);
    const [needsPasswordChange, setNeedsPasswordChange] = useState(false);
    
    const onSubmit = async (data: LoginFormInputs) => {
        const result = await dispatch(loginUser(data)) as { 
            meta: { requestStatus: string }, 
            payload: LoginResult 
        };

        if (result.meta.requestStatus === 'fulfilled') {
            // עכשיו אפשר לגשת ל-needsPasswordChange
            console.log('Login result:', result.payload);
            console.log('Needs password change:', result.payload.needsPasswordChange);
            
            if (result.payload.needsPasswordChange) {
                setNeedsPasswordChange(true);
            } else {
                onLogin();
                navigate('/courses');
            }
        }
    };

    const onChangePasswordSubmit = async (data: ChangePasswordInputs) => {
        const result = await dispatch(changePassword(data.newPassword)) as { 
            meta: { requestStatus: string } 
        };

        if (result.meta.requestStatus === 'fulfilled') {
            // הצלחה בשינוי הסיסמה
            onLogin();
            navigate('/courses');
        } else {
            // טיפול בשגיאות אם השינוי נכשל
            console.error("Error changing password:", result);
        }
    };

    return (
        <Box 
            sx={{
                display: 'flex',
                flexDirection: 'column',
                alignItems: 'center',
                justifyContent: 'center',
                height: '100vh',
            }}
        >
            <Typography variant="h4" color="turquoise">Login</Typography>
            <Box 
                component="form" 
                onSubmit={handleSubmit(onSubmit)} 
                sx={{
                    backgroundColor: 'white',
                    borderRadius: 2,
                    padding: 3,
                    boxShadow: 3,
                    width: '300px',
                }}
            >
                <TextField
                    label="Email"
                    variant="outlined"
                    fullWidth
                    margin="normal"
                    {...register('email', { 
                        required: 'Email is required', 
                        pattern: {
                            value: /@/,
                            message: 'Invalid email address'
                        }
                    })}
                    error={!!errors.email}
                    helperText={errors.email ? errors.email.message : ''}
                />
                <TextField
                    label="Password"
                    type="password"
                    variant="outlined"
                    fullWidth
                    margin="normal"
                    {...register('password', { 
                        required: 'Password is required', 
                        minLength: {
                            value: 4,
                            message: 'Password must be at least 4 characters long'
                        }
                    })}
                    error={!!errors.password}
                    helperText={errors.password ? errors.password.message : ''}
                />
                <Button type="submit" variant="contained" color="primary" disabled={loading}>
                    {loading ? <CircularProgress size={24} /> : 'Login'}
                </Button>
                {error && <Typography color="error">{error}</Typography>}
            </Box>
            {needsPasswordChange && (
                <Box 
                    component="form" 
                    onSubmit={handleSubmitChangePassword(onChangePasswordSubmit)} 
                    sx={{
                        backgroundColor: 'white',
                        borderRadius: 2,
                        padding: 3,
                        boxShadow: 3,
                        width: '300px',
                        marginTop: 2,
                    }}
                >
                    <TextField
                        label="New Password"
                        type="password"
                        variant="outlined"
                        fullWidth
                        margin="normal"
                        {...registerChangePassword('newPassword', { 
                            required: 'New password is required', 
                            minLength: {
                                value: 4,
                                message: 'Password must be at least 4 characters long'
                            }
                        })}
                        error={!!errorsChangePassword.newPassword}
                        helperText={errorsChangePassword.newPassword ? errorsChangePassword.newPassword.message : ''}
                    />
                    
                    <Button type="submit" variant="contained" color="primary">
                        Change Password
                    </Button>
                </Box>
            )}
        </Box>
    );
};

export default Login;