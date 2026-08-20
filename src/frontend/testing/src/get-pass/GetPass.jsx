import { useState, useEffect } from "react";
import SeatingZoneCard from "./SeatingZoneCard";

const GetPass = () => {

    const [form, setForm] = useState({firstName: "", lastName:"", address:"", city:"", zip:"", country:"", email:""});
    const [initialInput, setInitialInput] = useState('border border-2 border-blue-800 focus:outline-none w-full p-3 rounded-md focus:border-blue-600 focus:transition-[400ms] transition-[400ms]')
    const [wrongInput, setWrongInput] = useState('border border-2 border-red-800 focus:outline-none w-full p-3 rounded-md focus:border-red-600 focus:transition-[400ms] transition-[400ms]')
    const [inputStatus, setInputStatus] = useState([false, false, false, false, false, false, false])
    const [buttonLock, setButtonLock] = useState(true);
    const [lockedButtonStyle, setLockedButtonStyle] = useState("bg-gray-800 min-w-[10rem] min-h-[3rem] cursor-pointer rounded-md hover:bg-gray-700 transition hover:duration-400 duration-400");
    const [unlockedButtonStyle, setUnlockedButtonStyle] = useState("bg-blue-600 min-w-[10rem] min-h-[3rem] cursor-pointer rounded-md hover:bg-blue-700 transition hover:duration-400 duration-400");


    const handleChange = (field) => (e) => {
        const latestState = {...form};
        latestState[field] = e.target.value;
        setForm(latestState)

        if(field === 'name'){
            if(e.target.value.length == 0){
                e.target.className = wrongInput;
                const stat = [...inputStatus]
                stat[0] = false;
                setInputStatus(stat);
            }else{
                e.target.className = initialInput;
                const stat = [...inputStatus]
                stat[0] = true;
                setInputStatus(stat);
            }
        }

        if(field === 'surname'){
            if(e.target.value.length == 0){
                e.target.className = wrongInput;
                const stat = [...inputStatus]
                stat[1] = false;
                setInputStatus(stat);
            }else{
                e.target.className = initialInput;
                const stat = [...inputStatus]
                stat[1] = true;
                setInputStatus(stat);
            }
        }

        if(field === 'address'){
            if(e.target.value.length == 0){
                e.target.className = wrongInput;
                const stat = [...inputStatus]
                stat[2] = false;
                setInputStatus(stat);
            }else{
                e.target.className = initialInput;
                const stat = [...inputStatus]
                stat[2] = true;
                setInputStatus(stat);
            }
        }

        if(field === 'email'){
            if(!e.target.value.includes('@') || !e.target.value.includes('.com')){
                e.target.className = wrongInput;
                const stat = [...inputStatus]
                stat[6] = false;
                setInputStatus(stat);
            }else{
                e.target.className = initialInput;
                const stat = [...inputStatus]
                stat[6] = true;
                setInputStatus(stat);
            }
        }

        if(field === 'city'){
            if(e.target.value.length < 2){
                e.target.className = wrongInput;
                const stat = [...inputStatus]
                stat[3] = false;
                setInputStatus(stat);
            }
            else{
                e.target.className = initialInput;
                const stat = [...inputStatus];
                stat[3] = true;
                setInputStatus(stat);
            }
        }

        if(field === 'country'){
            if(e.target.value.length < 2){
                e.target.className = wrongInput;
                const stat = [...inputStatus]
                stat[5] = false;
                setInputStatus(stat);
            }
            else{
                e.target.className = initialInput;
                const stat = [...inputStatus];
                stat[5] = true;
                setInputStatus(stat);
            }
        }

        if(field === 'zip'){
            if(e.target.value.length < 5){
                e.target.className = wrongInput;
                const stat = [...inputStatus]
                stat[4] = false;
                setInputStatus(stat);
            }
            else{
                e.target.className = initialInput;
                const stat = [...inputStatus]
                stat[4] = true;
                console.log(stat)
                setInputStatus(stat);
            }
        }

       
        
        console.log(inputStatus)
        console.log('ButtonLock ' + buttonLock)
    }

    useEffect( () => {
    
        const btnLock = inputStatus.every(idx => idx === true)
        setButtonLock(!btnLock);

    }, [inputStatus])

    const emailValidation = () => {
        form.email.includes("@") && form.email.includes(".com");
    }

    const validateInputs = () => {
        const emailField = document.querySelector('#getpass-email');
        const passField = document.querySelector('#getpass-password');
        const cityField = document.querySelector('#getpass-city');
        const zipField = document.querySelector('#getpass-zip');
    }
    

    return(
        <div className="flex flex-row justify-center px-4 gap-3" id="getpass-overall-container">
            
            <div className="flex flex-col text-white bg-slate-900 w-2/3 max-w-3xl p-5 rounded-2xl">
                <h1 className="mb-4">Unesite podatke</h1>

                <div className="flex flex-col gap-5" id="getpass-input fields">
                    <input onChange={handleChange('name')} id="getpass-name" className={initialInput} type="text" placeholder="First name"/>
                    <input onChange={handleChange('surname')} id="getpass-surname" className={initialInput} type="text" placeholder="Last name"/>
                    <input onChange={handleChange('address')} id="getpass-address" className={initialInput} type="text" placeholder="Address"/>
                    <input onChange={handleChange('city')} id="getpass-city" className={initialInput} type="text" placeholder="City"/>
                    <input onChange={handleChange('zip')} id="getpass-zip" className={initialInput} type="text" placeholder="Zipcode"/>
                    <input onChange={handleChange('country')} id="getpass-country" className={initialInput} type="text" placeholder="Country"/>
                    <input onChange={handleChange('email')} id="getpass-email" className={initialInput} type="text" placeholder="Email"/>
                    <button onClick={validateInputs} className={buttonLock ? lockedButtonStyle : unlockedButtonStyle}>
                        {buttonLock ? "Locked" : "Continue"}
                    </button>
                </div>
            </div>

            {!buttonLock ? (<div className="flex flex-col justify-between text-white bg-gray-100 w-2/3 max-w-3xl p-5 rounded-2xl">
                 <div>
                        <h1 className="mb-4 text-slate-950">Choose seating zones:</h1>
                        <div className="flex flex-col gap-5">
                            <SeatingZoneCard></SeatingZoneCard>
                            <SeatingZoneCard></SeatingZoneCard>
                            <SeatingZoneCard></SeatingZoneCard>
                        </div>
                 </div>
                 <button className="bg-blue-600 min-w-[10rem] min-h-[3rem] cursor-pointer rounded-md hover:bg-blue-700 transition hover:duration-400 duration-400">Buy ticket</button>
            </div>) : <></>}

        </div>
    );
}

export default GetPass;