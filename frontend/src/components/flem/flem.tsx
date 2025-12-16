import { useAverageFlem, useFlem, useFlems } from "@/hooks/useFlem"

export const Flem = () => {  

    const { data: newFlem, refetch } = useFlem()
    const { data, isLoading, error } = useFlems(`${newFlem}`)
    const { data: average, isLoading: isLoadingAverage } = useAverageFlem(`${newFlem}`)

  if(isLoading) {
    return <div>Loading...</div>
  }

  if(error) {
    return <div className="font-bold">Could Not get Flem Reason? {error.message}</div>
  }

  return (
    <>
    <div className="font-bold">Average Flem: {isLoadingAverage ? '' : average}</div>
    <button className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded" onClick={() => refetch()}>
            Gen Flem
    </button>
    <div>Last flem : {newFlem}</div>
    <div className="flex">
        {
            data?.map(flem => 
                <div className="font-bold">{ flem.flemRate }</div>
            )
        }
    </div>
    </>
  )
}

