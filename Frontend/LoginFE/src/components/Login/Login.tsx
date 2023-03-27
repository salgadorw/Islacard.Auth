import { useState } from 'react';
import reactLogo from './assets/react.svg';
import { Button } from 'react-bootstrap';
import Form from 'react-bootstrap/Form';
export {Login};

function Login(){
    const [formData, setFormData] = useState({userName: "", password: ""})

    const handleSubmit = (e: React.SyntheticEvent)=> {        
        e.preventDefault();       
        async function fetchData(){              
            const response = await fetch('https://localhost:7260/api/Auth', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
                body: JSON.stringify(formData)
            });
            if(response.ok)
                window.location.href = "http://localhost:8000?token=Bearer "+await response.json();
            else {
                console.log(response);
                alert("ERROR:"+response.status);
                
            }
        }     
        fetchData();
      }
      function handleChange (e: React.ChangeEvent<HTMLInputElement>) {
        setFormData({...formData, [e.target.id]: e.target.value})
        }
    return(
        <div>
            <div className="alert alert-info">
                UserName: admin@admin.com<br />
                Password: admin
            </div>
            <div className="card">
                <h4 className="card-header">Login</h4>
                <div className="card-body">
                    <Form onSubmit={handleSubmit}>
                        <Form.Group className="mb-3">
                            <Form.Label>Username</Form.Label>
                            <Form.Control id='userName' type='email' onChange={handleChange} placeholder='Enter Email' />
                           
                        </Form.Group>
                        <Form.Group  className="mb-3">
                            <Form.Label>Password</Form.Label>
                            <Form.Control id='password' type='password' onChange={handleChange} placeholder='Password' />
                            
                        </Form.Group>
                        <Button variant="primary" type='submit'>Login</Button>{' '}
                    </Form>
                </div>
            </div>
        </div>
    )

}