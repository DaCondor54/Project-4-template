import { useFlem } from "@/hooks/useFlem"

export const Flem = () => {  

  const { data: reason, isLoading, error } =  useFlem()

  if(isLoading) {
    return <div>Loading...</div>
  }

  if(error) {
    return <div className="font-bold">Could Not get Flem Reason? {error.message}</div>
  }


  return (
    <>
    <div className="font-bold">
      Hello World! : {reason}
    </div>
    </>
  )
}

