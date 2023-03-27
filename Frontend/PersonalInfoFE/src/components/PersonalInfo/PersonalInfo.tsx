import { useState,useEffect } from 'react';
export {PersonalInfo};

interface LoginDTO{
    id: string;
    userName: string;
    password: string;
}

function PersonalInfo(){

    useEffect(() => {
        // Update the document title using the browser API
        handleLoad();
      });

    const [personalData, setPersonalData] = useState([] as Array<LoginDTO>)

    const handleLoad = ()=> {        
        
        async function fetchData(){              
            const queryParameters = new URLSearchParams(window.location.search)
            let token = queryParameters.get('token'); 
           
            const response = await fetch('https://localhost:7260/api/LoginManager', {
            method: 'GET',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': token??""
            }
            });
            if(response.ok)
                setPersonalData(await response.json())
            else {
                console.log(response);                     
            }
        }     
        fetchData();
      }
    //   function handleChange (e: React.ChangeEvent<HTMLInputElement>) {
    //     setFormData({...formData, [e.target.id]: e.target.value})
    //     }
    return(
        <div>           
            <div className="card">
                <h4 className="card-header">Logins Info</h4>
                <div className="card-body">
                {personalData.length > 0 && (
                <ul>
                {personalData.map(personalInfo => (
                    <li key={personalInfo.id}>Username:{personalInfo.userName}, Password:{personalInfo.password}</li>
                ))}
                </ul>
                )}
                </div>
            </div>
        </div>
    )

}