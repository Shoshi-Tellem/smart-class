import React from 'react';
import { useAppDispatch, useAppSelector } from '../stores/store';
import { useForm } from 'react-hook-form';
import { TextField, Button, CircularProgress, Typography, Box } from '@mui/material';
import { loginUser } from '../stores/authStore';
import { useNavigate } from 'react-router-dom'; // ייבוא useNavigate

interface LoginFormInputs {
    email: string;
    password: string;
}

interface LoginProps {
    onLogin: () => void;
}

const Login: React.FC<LoginProps> = ({ onLogin }) => {
    const dispatch = useAppDispatch();
    const navigate = useNavigate(); // יצירת משתנה navigate
    const { register, handleSubmit, formState: { errors } } = useForm<LoginFormInputs>();
    const loading = useAppSelector((state) => state.auth.loading);
    const error = useAppSelector((state) => state.auth.error);

    const onSubmit = (data: LoginFormInputs) => {
        dispatch(loginUser(data)).then(() => {
            onLogin(); // קריאה לפונקציה onLogin
            navigate('/courses'); // מעבר לעמוד הקורסים
        });
    };

    return (
        <Box 
            sx={{
                display: 'flex',
                flexDirection: 'column',
                alignItems: 'center',
                justifyContent: 'center',
                height: '100vh', // גובה המסך
            }}
        >
            <Typography variant="h4" color="turquoise">Login</Typography>
            <Box 
                component="form" 
                onSubmit={handleSubmit(onSubmit)} 
                sx={{
                    backgroundColor: 'white', // צבע רקע של הטופס
                    borderRadius: 2,
                    padding: 3,
                    boxShadow: 3,
                    width: '300px', // רוחב הטופס
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
        </Box>
    );
};

export default Login;